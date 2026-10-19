using NovoProjetoAvalonia.Services;
using NovoProjetoAvalonia.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NovoProjetoAvalonia.Models
{
    internal class Personagem : Client 
    {
        private long id;
        private String nomePersonagem;
        private bool logged = false;

        public Personagem(String nomePersonagem )
        {
            this.nomePersonagem = nomePersonagem;
        }

        public long getId()
        {
            return this.id;
        }

        public void desanexar()
        {
            MemoryManager.CloseHandle(this.HProcess);
        }

        public byte[] getIdBytes()
        {
            return BitConverter.GetBytes(this.id);
        }


        public void setLogged(bool logged)
        {
            this.logged = logged;
        }

        public IntPtr getHostPlayer()
        {
            return this.HostPlayer;
        }

        public IntPtr getHProcess()
        {
            return this.HProcess;
        }

        public IntPtr getBaseAddress()
        {
            return this.BaseAddress;
        }

        public Process getProcess()
        {
            return this.processoAnexado;
        }

        public byte[] getPacketStruct()
        {
            return this.packetStruct;
        }

        public byte[] getPacketSender()
        {
            return this.packetSender;
        }

        public Byte[] getFuncaoSeguir()
        {
            return this.funcaoSeguir;
        }

        public byte[] getFuncaoAceitar()
        {
            return this.funcaoAceitar;
        }

        public byte[] getFuncaoMandarGrupo() 
        {
            return this.funcaoMandarGrupo;
        }

        public byte[] getFuncaoKickarGrupo()
        {
            return this.funcaoKickarGrupo;
        }

        public byte[] getFuncaoPassarLider()
        {
            return this.funcaoPassarLider;
        }

        public byte[] getFuncaoSairGrupo()
        {
            return this.funcaoSairGrupo;
        }

        public byte[] getFuncaoTarget()
        {
            return this.funcaoTarget;
        }

        public byte[] getFuncaoCastar()
        {
            return this.funcaoCastar;
        }

        public byte[] getFuncaoMoveTo()
        {
            return this.funcaoMoveTo;
        }

        public byte[] getHostPlayerBytes()
        {
            return this.HostPlayerBytes;
        }

        public byte[] getFuncaoTeleporte()
        {
            return this.funcaoTeleporte;
        }

        public byte[] getFuncaoPegarMissao()
        {
            return this.funcaoPegarMissao;
        }

        public long getParty()
        {
            return this.party;
        }

        public long getCasting()
        {
            return MemoryManager.ReadByte(this.HProcess, this.isCasting +0x804);
        }

        public async Task<bool> AnexarProcessoAsync(Action<string> onNomeLido = null)
        {
            // Roda em segundo plano para não congelar a interface (View/Avalonia)
            return await Task.Run(async () =>
            {
                while (true)
                {
                    Process[] processes = Process.GetProcessesByName("elementclient_64");

                    foreach (Process processo in processes)
                    {
                        IntPtr hProcess = IntPtr.Zero;
                        try
                        {
                            Process proc = processo;
                            hProcess = MemoryManager.OpenProcess(0x001F0FFF, false, proc.Id);


                            IntPtr baseAddress = proc.MainModule.BaseAddress;


                            if (hProcess == IntPtr.Zero)
                                continue;

                            IntPtr hostPlayer = (IntPtr)MemoryManager.ReadInt64(hProcess, (IntPtr)MemoryManager.ReadInt64(hProcess, baseAddress + 0x01be7c18) + 0x8);


                            IntPtr ptr1 = (IntPtr)MemoryManager.ReadInt64(hProcess, baseAddress + 0x01bece00);
                            IntPtr enderecoNome = ptr1 + 0x0;
                            string nome = MemoryManager.ReadUnicodeString(hProcess, enderecoNome, 48);

                            onNomeLido?.Invoke(nome);

                            // 2. Compara se é o personagem esperado nesta janela
                            if (!string.IsNullOrEmpty(nome) && nome.Equals(this.nomePersonagem))
                            {

                                this.processoAnexado = processo;
                                this.BaseAddress = baseAddress;
                                this.HProcess = hProcess;
                                this.HostPlayer = (IntPtr)((IntPtr)hostPlayer + 0);
                                //this.id = MemoryManager.ReadInt64(hProcess, new IntPtr(hostPlayer + 0x948));
                                this.nomePersonagem = nome;
                                //long ptr1 = MemoryManager.ReadInt64(hProcess, new IntPtr(baseAddress + 0x1713230));
                                //this.party = ptr1;

                                this.HostPlayerBytes = BitConverter.GetBytes(hostPlayer);
                                this.funcaoSeguir = BitConverter.GetBytes(baseAddress + 0xab6e60);
                                this.funcaoAceitar = BitConverter.GetBytes(baseAddress + 0xcfe2f0);
                                this.funcaoMandarGrupo = BitConverter.GetBytes(baseAddress + 0xcfe5c0);
                                this.funcaoKickarGrupo = BitConverter.GetBytes(baseAddress + 0xcfe630);
                                this.funcaoPassarLider = BitConverter.GetBytes(baseAddress + 0xcfe430);
                                this.funcaoSairGrupo = BitConverter.GetBytes(baseAddress + 0xcfe690);
                                this.funcaoTarget = BitConverter.GetBytes(baseAddress + 0xcfe3d0);
                                this.funcaoTeleporte = BitConverter.GetBytes(baseAddress + 0xD5F360);
                                this.funcaoPegarMissao = BitConverter.GetBytes(baseAddress + 0xcf6090);
                                this.packetSender = BitConverter.GetBytes(baseAddress + 0xe0d010);

                                this.funcaoMoveTo = BitConverter.GetBytes(baseAddress + 0x8ac2c0);
                                this.funcaoCastar = BitConverter.GetBytes(baseAddress + 0x8b4880);

                                long pStruct = MemoryManager.ReadInt64(hProcess, new IntPtr(baseAddress + 0x1becd78));
                                long pStruct2 = MemoryManager.ReadInt64(hProcess, new IntPtr(pStruct + 0x40));
                                this.packetStruct = BitConverter.GetBytes(pStruct2);



                                return true; // Sucesso
                            }
                        }
                        catch
                        {

                        }
                        finally
                        {
                            if (this.processoAnexado != processo && hProcess != IntPtr.Zero)
                            {
                                MemoryManager.CloseHandle(hProcess);
                            }
                        }
                    }
                    await Task.Delay(2000);
                }
            });
        }





        //public async Task<bool> AnexarProcessoAsync(Action<string> onNomeLido = null)
        //{
        //    // Roda em segundo plano para não congelar a interface (View/Avalonia)
        //    return await Task.Run(async () =>
        //    {
        //        while (true)
        //        {
        //            Process[] processes = Process.GetProcessesByName("elementclient_64");

        //            foreach (Process processo in processes)
        //            {
        //                IntPtr hProcess = IntPtr.Zero;
        //                try
        //                {
        //                    Process proc = processo;
        //                    hProcess = MemoryManager.OpenProcess(0x001F0FFF, false, proc.Id);
        //                    IntPtr baseAddress = proc.MainModule.BaseAddress;


        //                    if (hProcess == IntPtr.Zero)
        //                        continue;

        //                    IntPtr hostPlayer = (IntPtr)MemoryManager.ReadInt64(hProcess, (IntPtr)MemoryManager.ReadInt64(hProcess, baseAddress + 0x01734248) + 0x60);
        //                    IntPtr ptr3 = (IntPtr)MemoryManager.ReadInt64(hProcess, hostPlayer + 0xC88);
        //                    IntPtr enderecoNome = ptr3 + 0x0;
        //                    string nome = MemoryManager.ReadUnicodeString(hProcess, enderecoNome, 48);

        //                    onNomeLido?.Invoke(nome);

        //                    // 2. Compara se é o personagem esperado nesta janela
        //                    if (!string.IsNullOrEmpty(nome) && nome.Equals(this.nomePersonagem))
        //                    {

        //                        this.processoAnexado = processo;
        //                        this.BaseAddress = baseAddress;
        //                        this.HProcess = hProcess;
        //                        this.HostPlayer = (IntPtr)((IntPtr)hostPlayer + 0);
        //                        this.id = MemoryManager.ReadInt64(hProcess, new IntPtr(hostPlayer + 0x8C0));
        //                        this.nomePersonagem = nome;
        //                        long ptr1 = MemoryManager.ReadInt64(hProcess, new IntPtr(baseAddress + 0x1713230));
        //                        this.party = ptr1;

        //                        this.HostPlayerBytes = BitConverter.GetBytes(HostPlayer);
        //                        this.funcaoSeguir = BitConverter.GetBytes(baseAddress + 0x8B5F60);
        //                        this.funcaoAceitar = BitConverter.GetBytes(baseAddress + 0xA7EBE0);
        //                        this.funcaoMandarGrupo = BitConverter.GetBytes(baseAddress + 0xA7ED80);
        //                        this.funcaoKickarGrupo = BitConverter.GetBytes(baseAddress + 0xA7EDF0);
        //                        this.funcaoPassarLider = BitConverter.GetBytes(baseAddress + 0xA7ED20);
        //                        this.funcaoSairGrupo = BitConverter.GetBytes(baseAddress + 0xA7EE50);
        //                        this.funcaoTarget = BitConverter.GetBytes(baseAddress + 0xA7ECC0);
        //                        this.funcaoCastar = BitConverter.GetBytes(baseAddress + 0x8b4880);
        //                        this.funcaoTeleporte = BitConverter.GetBytes(baseAddress + 0xacf510);
        //                        this.funcaoMoveTo = BitConverter.GetBytes(baseAddress + 0x8ac2c0);
        //                        this.funcaoPegarMissao = BitConverter.GetBytes(baseAddress + 0xA76F50);
        //                        this.packetSender = BitConverter.GetBytes(baseAddress + 0xb4c010);

        //                        long pStruct = MemoryManager.ReadInt64(hProcess, new IntPtr(baseAddress + 0x172c8e0));
        //                        long pStruct2 = MemoryManager.ReadInt64(hProcess, new IntPtr(pStruct + 0x40));

        //                        this.packetStruct = BitConverter.GetBytes(pStruct2);



        //                        long ptrCasting = MemoryManager.ReadInt64(hProcess, new IntPtr(baseAddress + 0x1713238));
        //                        long isCastingPtr = MemoryManager.ReadInt64(hProcess, new IntPtr(ptrCasting + 0x40));
        //                        long isCastingPtr2 = MemoryManager.ReadInt64(hProcess, new IntPtr(isCastingPtr + 0x170));
        //                        this.isCasting = new IntPtr(isCastingPtr2);


        //                        return true; // Sucesso
        //                    }
        //                }
        //                catch
        //                {

        //                }
        //                finally
        //                {
        //                    if (this.processoAnexado != processo && hProcess != IntPtr.Zero)
        //                    {
        //                        MemoryManager.CloseHandle(hProcess);
        //                    }
        //                }
        //            }
        //            await Task.Delay(2000);
        //        }
        //    });
        //}
    }
}
