using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NovoProjetoAvalonia.Services
{
    
    internal class Hooks
    {
        MemoryManager memoryManager = new MemoryManager();

        private CancellationTokenSource? _cts;
        private bool _verificacaoAtiva = false;

        
        public IntPtr InstalarHookR14(IntPtr baseAddress, IntPtr hpProcess,out IntPtr enderecoSharedMem)
        {
            long hookAddress = baseAddress.ToInt64() + 0xC75890;

            // O endereço exato do Code Cave que encontraste no disassembler
            long caveOffset = 0x0C75897;
            IntPtr caveAddress = new IntPtr(baseAddress.ToInt64() + caveOffset);

            // 1. Alocar apenas a memória partilhada (a única que precisa de ser criada externamente)
            enderecoSharedMem = MemoryManager.VirtualAllocEx(hpProcess, IntPtr.Zero, 8, 0x1000 | 0x2000, 0x40);

            if (enderecoSharedMem == IntPtr.Zero)
            {
               
                return IntPtr.Zero;
            }

            // 2. Preparar os dados para o shellcode
            byte[] sharedMemBytes = BitConverter.GetBytes(enderecoSharedMem.ToInt64());
            long returnAddress = hookAddress + 5; // Endereço de retorno após os 5 bytes do JMP original
            byte[] returnBytes = BitConverter.GetBytes(returnAddress);

            // Calcular o deslocamento relativo (Relative JMP) do gancho para o code cave
            int relativeOffsetToCave = (int)(caveAddress.ToInt64() - (hookAddress + 5));

            // 3. Montar o Shellcode
            List<byte> shellcode = new List<byte>();

            // push rax (preserva rax)
            shellcode.Add(0x50);

            // mov rax, <enderecoSharedMem>
            shellcode.Add(0x48); shellcode.Add(0xB8);
            shellcode.AddRange(sharedMemBytes);

            // mov [rax], r14 (salva o valor atual de r14 na memória partilhada)
            shellcode.Add(0x4C); shellcode.Add(0x89); shellcode.Add(0x30);

            // pop rax (restaura rax)
            shellcode.Add(0x58);

            // Instruções originais sobrescritas: add rsp, 30; pop rdi
            shellcode.AddRange(new byte[] { 0x48, 0x83, 0xC4, 0x30, 0x5F });

            // jmp <returnAddress> (Salto absoluto de volta para o fluxo normal do jogo)
            shellcode.Add(0x48); shellcode.Add(0xB8);
            shellcode.AddRange(returnBytes);
            shellcode.Add(0xFF); shellcode.Add(0xE0);

            // 4. Escrever o shellcode diretamente no Code Cave encontrado (140C75A72)
            // Mudamos a proteção da página do Code Cave para PAGE_EXECUTE_READWRITE (0x40)
            MemoryManager.VirtualProtectEx(hpProcess, caveAddress, (uint)shellcode.Count, 0x40, out uint oldProtectCave);
            MemoryManager.WriteProcessMemory(hpProcess, caveAddress, shellcode.ToArray(), (uint)shellcode.Count, out _);
            MemoryManager.VirtualProtectEx(hpProcess, caveAddress, (uint)shellcode.Count, oldProtectCave, out _);

            // 5. Aplicar o JMP no endereço original do jogo para redirecionar para o Code Cave
            MemoryManager.VirtualProtectEx(hpProcess, (IntPtr)hookAddress, 5, 0x40, out uint oldProtectHook);

            List<byte> patch = new List<byte>();
            patch.Add(0xE9); // JMP rel32
            patch.AddRange(BitConverter.GetBytes(relativeOffsetToCave));

            MemoryManager.WriteProcessMemory(hpProcess, (IntPtr)hookAddress, patch.ToArray(), (uint)patch.Count, out _);
            MemoryManager.VirtualProtectEx(hpProcess, (IntPtr)hookAddress, 5, oldProtectHook, out _);

            return caveAddress;
        }


        public void DesinstalarHookR14(IntPtr baseAddress, IntPtr hpProcess,IntPtr caveAddress, IntPtr enderecoSharedMem, byte[] bytesOriginaisHook)
        {
            long hookAddress = baseAddress.ToInt64() + 0xC75890;

            // 1. Restaurar os bytes originais no endereço do gancho
            if (bytesOriginaisHook != null && bytesOriginaisHook.Length == 5)
            {
                MemoryManager.VirtualProtectEx(hpProcess, (IntPtr)hookAddress, 5, 0x40, out uint oldProtect);
                MemoryManager.WriteProcessMemory(hpProcess, (IntPtr)hookAddress, bytesOriginaisHook, 5, out _);
                MemoryManager.VirtualProtectEx(hpProcess, (IntPtr)hookAddress, 5, oldProtect, out _);
            }

            // 2. Limpar o Code Cave (opcional: preencher com 0xCC novamente)
            if (caveAddress != IntPtr.Zero)
            {
                // O nosso shellcode tem cerca de 50 bytes, limpamos uma área segura de 100 bytes
                byte[] bufferLimpeza = new byte[100];
                for (int i = 0; i < bufferLimpeza.Length; i++) bufferLimpeza[i] = 0xCC;

                MemoryManager.VirtualProtectEx(hpProcess, caveAddress, (uint)bufferLimpeza.Length, 0x40, out uint oldProtectCave);
                MemoryManager.WriteProcessMemory(hpProcess, caveAddress, bufferLimpeza, (uint)bufferLimpeza.Length, out _);
                MemoryManager.VirtualProtectEx(hpProcess, caveAddress, (uint)bufferLimpeza.Length, oldProtectCave, out _);
            }

            // 3. Liberar a memória compartilhada alocada no jogo
            if (enderecoSharedMem != IntPtr.Zero)
            {
                // 0x8000 = MEM_RELEASE. Quando usamos MEM_RELEASE, o tamanho deve ser 0.
                MemoryManager.VirtualFreeEx(hpProcess, enderecoSharedMem, 0, 0x8000);
            }
        }

        public void IniciarMonitoramento(IntPtr baseAddress, IntPtr hpProcess, IntPtr enderecoSharedMem)
        {

            _cts = new CancellationTokenSource();



            // Roda a tarefa em uma thread separada em segundo plano
            Task.Run(async () =>
            {
                // Buffer de 8 bytes (tamanho de um registrador de 64 bits / long)
                byte[] buffer = new byte[8];

                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        // Lê diretamente da memória do jogo o valor que o gancho atualizou
                        bool sucesso = MemoryManager.ReadProcessMemory(hpProcess, enderecoSharedMem, buffer, buffer.Length, out _);

                        if (sucesso)
                        {
                            // Converte os bytes lidos para um número inteiro de 64 bits (long)
                            long valorR14 = BitConverter.ToInt64(buffer, 0);

                            // Verifica se atingiu a condição desejada
                            if (valorR14 == 0xEC1)
                            {

                                await Task.Delay(200);
                                //await Task.Delay(80);
                                // Dispara a sua função de cancelar skill com segurança no C#
                                // (Substitua pelos parâmetros reais que você usa)
                                //InternFunctions.testeCancelarSkill(0xf85);

                                //await Task.Delay(80);
                                // Adiciona um pequeno intervalo ou cooldown para evitar disparos 
                                // múltiplos em loop enquanto o r14 se mantiver igual a 0x23C
                                //await Task.Delay(2000, _cts.Token);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Trata eventuais exceções de leitura (caso o processo feche, etc.)
                    }

                }
            }, _cts.Token);
        }
    }
}
