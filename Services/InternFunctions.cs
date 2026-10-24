using NovoProjetoAvalonia.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NovoProjetoAvalonia.Services
{
    internal class InternFunctions
    {

        private CancellationTokenSource? _cts;
        private bool _verificacaoAtiva = false;


        public static void seguir(IntPtr HProcess, IntPtr sharedAllocMem, byte[] hostPlayer, byte[] funcaoAddress, byte[] playerID)
        {

            {

                byte[] shellcode = new byte[]
                {
            
                    0x48, 0x83, 0xEC, 0x30,                
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                             
                    0x48, 0x83, 0xC4, 0x30,                
                    0xC3,                                 

                };
                Buffer.BlockCopy(hostPlayer, 0, shellcode, 6, 8);
                Buffer.BlockCopy(playerID, 0, shellcode, 16, 8);
                Buffer.BlockCopy(funcaoAddress, 0, shellcode, 26, 8);

                if (MemoryManager.WriteProcessMemory(HProcess, sharedAllocMem, shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, sharedAllocMem, IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }


        public static void aceitar(IntPtr HProcess, IntPtr sharedAllocMem, long partyAddress, byte[] funcaoAddress, byte [] playerID)
        {

            {

                long ptr1 = MemoryManager.ReadInt64(HProcess, new IntPtr(partyAddress + 0x20));
                long ptr2 = ptr1 + 0x0;


                byte[] partyBytes = BitConverter.GetBytes(ptr2);

                byte[] shellcode = new byte[]
                {
               
                    0x48, 0x83, 0xEC, 0x28,                
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xBA, 0x98, 0xDE, 0x15, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0xFF, 0xD0,                                               
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                               

                };

                Buffer.BlockCopy(playerID, 0, shellcode, 6, 8);
                Buffer.BlockCopy(partyBytes, 0, shellcode, 16, 8);
                Buffer.BlockCopy(funcaoAddress, 0, shellcode, 26, 8);


                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 100), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 100), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }


        public static void mandarGrupo(IntPtr HProcess, IntPtr sharedAllocMem, byte[]funcao, byte[] playerID)
        {

            {

                byte[] shellcode = new byte[]
                {
             
                    0x48, 0x83, 0xEC, 0x28,                
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                                  
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                                

                };
                Buffer.BlockCopy(playerID, 0, shellcode, 6, 8);
                Buffer.BlockCopy(funcao, 0, shellcode, 16, 8);


                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 200), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 200), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }

        public static void kickarGrupo(IntPtr HProcess, IntPtr sharedAllocMem, byte[] funcao ,byte[] playerID)
        {

            {

                byte[] shellcode = new byte[]
                {

                    0x48, 0x83, 0xEC, 0x28,                
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                                 
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                                  

                };

                Buffer.BlockCopy(playerID, 0, shellcode, 6, 8);
                Buffer.BlockCopy(funcao, 0, shellcode, 16, 8);

                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 300), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 300), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }

        public static void passarLider(IntPtr HProcess, IntPtr sharedAllocMem, byte[] funcao, byte[] playerID)
        {

            {

                byte[] shellcode = new byte[]
                {

                    0x48, 0x83, 0xEC, 0x28,               
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                                  
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                                  

                };

                Buffer.BlockCopy(playerID, 0, shellcode, 6, 8);
                Buffer.BlockCopy(funcao, 0, shellcode, 16, 8);

                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 400), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 400), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }


        public static void sairGrupo(IntPtr HProcess, IntPtr sharedAllocMem, byte[] funcao)
        {

            {

                byte[] shellcode = new byte[]
                {
                
                    0x48, 0x83, 0xEC, 0x28,                
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                                  
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                                 
                };

                Buffer.BlockCopy(funcao, 0, shellcode, 6, 8);

                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 500), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 500), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }

        public static void pegarTarget(IntPtr HProcess, IntPtr sharedAllocMem, byte[] funcao ,byte[] playerID)
        {

            {

                byte[] shellcode = new byte[]
                {

                    0x48, 0x83, 0xEC, 0x28,                
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                                  
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                                  

                };
                Buffer.BlockCopy(playerID, 0, shellcode, 6, 8);
                Buffer.BlockCopy(funcao, 0, shellcode, 16, 8);


                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 600), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 600), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }


        public static void teleporte(IntPtr HProcess, IntPtr sharedAllocMem, IntPtr buffer, byte[] packetStruct, byte[] packetSend, long npcID, long slotItem)
        {

            {
                
                byte[] npcbyt = BitConverter.GetBytes(npcID);
                byte[] itemslot = BitConverter.GetBytes(slotItem);

                if (npcbyt.Length == 0 || itemslot.Length == 0)
                {
                    return;
                }

                byte[] teleportbuffer = new byte[] { 0x6d, 0x00, 0x00, itemslot[0], 0x00, 0x01, 0x00, 0x00, 0x00, 0x12, 0xE8, 0x00,0x00, npcbyt[0], npcbyt[1],0x00,0x00 };

               
                MemoryManager.WriteProcessMemory(HProcess, buffer, teleportbuffer, 0x11, out _);

                byte[] memBufferAddress = BitConverter.GetBytes(buffer);

                byte[] shellcode = new byte[]
                {

                    0x48, 0x83, 0xEC, 0x28,
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x49, 0xB8, 0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0xFF, 0xD0,
                    0x48, 0x83, 0xC4, 0x28,
                    0xC3,

                };
                Buffer.BlockCopy(packetStruct, 0, shellcode, 6, 8);
                Buffer.BlockCopy(memBufferAddress, 0, shellcode, 16, 8);
                Buffer.BlockCopy(packetSend, 0, shellcode, 36, 8);

                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 700), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 700), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }


        public static void skillSender(IntPtr HProcess, IntPtr sharedAllocMem, IntPtr buffer, IntPtr hostPlayer, byte[] packetStruct, byte[] packetSender, long idSkill)
        {

            long ptralvo = MemoryManager.ReadInt64(HProcess, new IntPtr(hostPlayer + 0xB14));

            byte[] alvo = BitConverter.GetBytes(ptralvo);

            byte[] skillid = BitConverter.GetBytes(idSkill);

            byte[] pacoteSkill = new byte[] { 0x29, 0x00, skillid[0], skillid[1], 0x00, 0x00, 0x0b, 0x01, alvo[0], alvo[1], alvo[2], alvo[3] };

            
            MemoryManager.WriteProcessMemory(HProcess, buffer, pacoteSkill, 0xC, out _);

            byte[] memb = BitConverter.GetBytes(buffer);

            
            byte[] shellcode = new byte[]
            {
                0x48, 0x83, 0xEC, 0x28,                     
                0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x49, 0xB8, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0xFF, 0xD0,
                0x48, 0x83, 0xC4, 0x28,
                0xC3
            };

            
            Buffer.BlockCopy(packetStruct, 0, shellcode, 6, 8);
            Buffer.BlockCopy(memb, 0, shellcode, 16, 8);
            Buffer.BlockCopy(packetSender, 0, shellcode, 36, 8);


            if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem+800), shellcode, (uint)shellcode.Length, out _))
            {
                // Cria a thread apontando para o bloco fixo
                IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 800), IntPtr.Zero, 0, IntPtr.Zero);
                if (hThread != IntPtr.Zero)
                {
                    MemoryManager.WaitForSingleObject(hThread, 100);
                    MemoryManager.CloseHandle(hThread);
                }
            }
        }

        public static void skillSenderFox(IntPtr HProcess, IntPtr sharedAllocMem, IntPtr buffer, IntPtr hostPlayer, byte[] packetStruct, byte[] packetSender, long idSkill)
        {

            long ptralvo = MemoryManager.ReadInt64(HProcess, new IntPtr(hostPlayer + 0xB14));

            byte[] alvo = BitConverter.GetBytes(ptralvo);

            byte[] skillid = BitConverter.GetBytes(idSkill);

            byte[] pacoteSkill = new byte[] { 0x29, 0x00, skillid[0], skillid[1], 0x00, 0x00, 0x01, 0x01, alvo[0], alvo[1], alvo[2], alvo[3] };


            MemoryManager.WriteProcessMemory(HProcess, buffer, pacoteSkill, 0xC, out _);

            byte[] memb = BitConverter.GetBytes(buffer);


            byte[] shellcode = new byte[]
            {
                0x48, 0x83, 0xEC, 0x28,
                0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x49, 0xB8, 0x0c, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0xFF, 0xD0,
                0x48, 0x83, 0xC4, 0x28,
                0xC3
            };


            Buffer.BlockCopy(packetStruct, 0, shellcode, 6, 8);
            Buffer.BlockCopy(memb, 0, shellcode, 16, 8);
            Buffer.BlockCopy(packetSender, 0, shellcode, 36, 8);


            if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 800), shellcode, (uint)shellcode.Length, out _))
            {
                // Cria a thread apontando para o bloco fixo
                IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 800), IntPtr.Zero, 0, IntPtr.Zero);
                if (hThread != IntPtr.Zero)
                {
                    MemoryManager.WaitForSingleObject(hThread, 100);
                    MemoryManager.CloseHandle(hThread);
                }
            }
        }

        public static void daimonSender(IntPtr HProcess, IntPtr sharedAllocMem, IntPtr buffer, IntPtr hostPlayer, byte[] packetStruct, byte[] packetSender)
        {

            long ptralvo = MemoryManager.ReadInt64(HProcess, new IntPtr(hostPlayer + 0xB14));

            byte[] alvo = BitConverter.GetBytes(ptralvo);

            byte[] pacoteSkill = new byte[] { 0x74, 0x00, 0xEA, 0X03, 0x01, 0x01, alvo[0], alvo[1], alvo[2], alvo[3]};


            MemoryManager.WriteProcessMemory(HProcess, buffer+20, pacoteSkill, 0xA, out _);

            byte[] memb = BitConverter.GetBytes(buffer+20);


            byte[] shellcode = new byte[]
            {
                0x48, 0x83, 0xEC, 0x28,
                0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x49, 0xB8, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0xFF, 0xD0,
                0x48, 0x83, 0xC4, 0x28,
                0xC3
            };


            Buffer.BlockCopy(packetStruct, 0, shellcode, 6, 8);
            Buffer.BlockCopy(memb, 0, shellcode, 16, 8);
            Buffer.BlockCopy(packetSender, 0, shellcode, 36, 8);


            if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 1100), shellcode, (uint)shellcode.Length, out _))
            {
                // Cria a thread apontando para o bloco fixo
                IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 1100), IntPtr.Zero, 0, IntPtr.Zero);
                if (hThread != IntPtr.Zero)
                {
                    MemoryManager.WaitForSingleObject(hThread, 100);
                    MemoryManager.CloseHandle(hThread);
                }
            }
        }


        public static void moveTo(IntPtr HProcess, IntPtr sharedAllocMem, IntPtr buffer, byte[] funcao, byte[] hostPlayer, float[] destino)
        {

            byte[] moveTo = new byte[0x40];


            byte[] coordx = BitConverter.GetBytes(destino[0]);
            coordx.CopyTo(moveTo, 24);
            byte[] coordy = BitConverter.GetBytes(destino[1]);
            coordy.CopyTo(moveTo, 28);
            coordy.CopyTo(moveTo, 32);

            byte[] coordz = BitConverter.GetBytes(destino[2]);
            coordz.CopyTo(moveTo, 36);
            coordz.CopyTo(moveTo, 40);

            moveTo[4] = 1;
            moveTo[16] = 3;
            byte[] triger = BitConverter.GetBytes(330);
            triger.CopyTo(moveTo);


            
            MemoryManager.WriteProcessMemory(HProcess, buffer, moveTo, 0x40, out _);

            byte[] memBufferBytes = BitConverter.GetBytes(buffer.ToInt64());


            byte[] shellcode = new byte[]
            {
                0x48, 0x83, 0xEC, 0x28,                
                0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                0xFF, 0xD0,                            
                0x48, 0x83, 0xC4, 0x28,                
                0xC3                                  
            };
            Buffer.BlockCopy(hostPlayer, 0, shellcode, 6, 8);
            Buffer.BlockCopy(memBufferBytes, 0, shellcode, 16, 8);
            Buffer.BlockCopy(funcao, 0, shellcode, 26, 8);


            if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 900), shellcode, (uint)shellcode.Length, out _))
            {
                // Cria a thread apontando para o bloco fixo
                IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 900), IntPtr.Zero, 0, IntPtr.Zero);
                if (hThread != IntPtr.Zero)
                {
                    MemoryManager.WaitForSingleObject(hThread, 100);
                    MemoryManager.CloseHandle(hThread);
                }
            }
        }


        public static void pegarMissao(IntPtr HProcess, IntPtr sharedAllocMem, IntPtr buffer, IntPtr baseAddress, byte[] packetStruct, byte[] packetSend, long npcID)
        {

            {

                byte[] missao = new byte[0x1a];

                byte[] npc = BitConverter.GetBytes((short)npcID);
                npc.CopyTo(missao, 0xa);

                byte[] prim = BitConverter.GetBytes((short)37);
                prim.CopyTo(missao, 0);

                missao[2] = 7;

                byte[] prim2 = BitConverter.GetBytes((short)16);
                prim2.CopyTo(missao, 6);


                MemoryManager.WriteProcessMemory(HProcess, buffer, missao, 0x1a, out _);

                byte[] memBufferAddress = BitConverter.GetBytes(buffer);

                byte[] shellcode = new byte[]
                {
                
                    0x48, 0x83, 0xEC, 0x28,                
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0x49, 0xB8, 0x1a, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x49, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                    0xFF, 0xD0,                                                  
                    0x48, 0x83, 0xC4, 0x28,                
                    0xC3,                                  

                };
                Buffer.BlockCopy(packetStruct, 0, shellcode, 6, 8);
                Buffer.BlockCopy(memBufferAddress, 0, shellcode, 16, 8);
                Buffer.BlockCopy(packetSend, 0, shellcode, 46, 8);


                if (MemoryManager.WriteProcessMemory(HProcess, (sharedAllocMem + 1000), shellcode, (uint)shellcode.Length, out _))
                {
                    // Cria a thread apontando para o bloco fixo
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, (sharedAllocMem + 1000), IntPtr.Zero, 0, IntPtr.Zero);
                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 100);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
            }
        }


        public static void Castar(IntPtr HProcess, IntPtr sharedAllocMem, byte[] funcao, byte[] hostPlayer, long skillID)
        {

            {
                byte[] skillId = BitConverter.GetBytes(skillID);



                byte[] shellcode = new byte[]
                {

                    0x48, 0x83, 0xEC, 0x28,
                    0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x49, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x49, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0xFF, 0xD0,
                    0x48, 0x83, 0xC4, 0x28,
                    0xC3,

                };
                Buffer.BlockCopy(hostPlayer, 0, shellcode, 6, 8);
                Buffer.BlockCopy(skillId, 0, shellcode, 16, 8);
                Buffer.BlockCopy(funcao, 0, shellcode, 46, 8);


                IntPtr allocMem = MemoryManager.VirtualAllocEx(HProcess, IntPtr.Zero, (uint)shellcode.Length, 0x1000 | 0x2000, 0x40);
                if (allocMem != IntPtr.Zero)
                {
                    if (MemoryManager.WriteProcessMemory(HProcess, allocMem, shellcode, (uint)shellcode.Length, out _))
                    {
                        IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);
                        if (hThread != IntPtr.Zero)
                        {
                            MemoryManager.WaitForSingleObject(hThread, 100);
                            MemoryManager.CloseHandle(hThread);
                        }
                    }
                    MemoryManager.VirtualFreeEx(HProcess, allocMem, 0, 0x8000);
                }
            }
        }

        public static void telarseminterface(IntPtr HProcess, IntPtr baseAddress, byte[] packetStruct, byte[] packetSend, long npcID)
        {

            {

                byte[] npcbyt = BitConverter.GetBytes(npcID);

                byte[] teleportbuffer = new byte[] { 0x6d, 0x00, 0x00, 0x01, 0x32, 0x00, 0x34, 0xc8,0x00,0x00, npcbyt[0], npcbyt[1]};

                IntPtr memBuffer = MemoryManager.VirtualAllocEx(HProcess, IntPtr.Zero, (UIntPtr)0xE, 0x1000 | 0x2000, 0x40);
                MemoryManager.WriteProcessMemory(HProcess, memBuffer, teleportbuffer, 0xE, out _);

                byte[] memBufferAddress = BitConverter.GetBytes(memBuffer);

                byte[] shellcode = new byte[]
                {

                0x48, 0x83, 0xEC, 0x28,


                0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x49, 0xB8, 0xE, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0xFF, 0xD0,

                0x48, 0x83, 0xC4, 0x28,
                0xC3,

                };
                Buffer.BlockCopy(packetStruct, 0, shellcode, 6, 8);
                Buffer.BlockCopy(memBufferAddress, 0, shellcode, 16, 8);
                Buffer.BlockCopy(packetSend, 0, shellcode, 36, 8);

                // Alocação e escrita na memória
                IntPtr allocMem = MemoryManager.VirtualAllocEx(HProcess, IntPtr.Zero, (uint)shellcode.Length, 0x1000 | 0x2000, 0x40);
                if (allocMem != IntPtr.Zero)
                {
                    if (MemoryManager.WriteProcessMemory(HProcess, allocMem, shellcode, (uint)shellcode.Length, out _))
                    {
                        IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);
                        if (hThread != IntPtr.Zero)
                        {
                            MemoryManager.WaitForSingleObject(hThread, 1000);
                            MemoryManager.CloseHandle(hThread);
                        }
                    }
                    MemoryManager.VirtualFreeEx(HProcess, allocMem, 0, 0x8000);
                    MemoryManager.VirtualFreeEx(HProcess, memBuffer, 0, 0x8000);
                }
            }
        }



        public static void testeTP(IntPtr HProcess, IntPtr baseAddress, long npcID)
        {
            // Converte o endereço da função para 8 bytes (Little-Endian)
            byte[] addressBytes = BitConverter.GetBytes(baseAddress + 0xA76F50);
            byte[] addressBytes2 = BitConverter.GetBytes(npcID);

            byte[] shellcode = new byte[]
            {
                // 1. sub rsp, 0x28 (Shadow Space + Alinhamento) [4 bytes]
                0x48, 0x83, 0xEC, 0x28,

                // 2. mov rcx, 0x6a0 (1º Argumento) [10 bytes]
                0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                // 3. mov rdx, 0xb (2º Argumento) [10 bytes]
                0x48, 0xBA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                // 4. mov r8, 1 (3º Argumento - 64 bits completo) [10 bytes]
                0x49, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                // 5. mov r9, 0x15D9C8 (4º Argumento - 64 bits completo) [10 bytes]
                0x49, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                // 6. mov rax, targetAddress (Endereço da função SkillCall) 
                // Inicia exatamente no índice 44 (0x2C) [2 bytes do opcode + 8 bytes de placeholder]
                0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

                // 7. call rax [2 bytes]
                0xFF, 0xD0,

                // 8. add rsp, 0x28 (Restaura a pilha) [4 bytes]
                0x48, 0x83, 0xC4, 0x28,

                // 9. ret [1 byte]
                0xC3
            };

            // Soma dos tamanhos acima até o 'mov rax': 4 + 10 + 10 + 10 + 10 = 44 bytes.
            // O opcode '0x48, 0xB8' ocupa os 2 primeiros bytes da instrução 6, logo os dados começam no índice 46.
            // Vamos copiar o endereço exatamente a partir do índice 46 (0x2E).
            Buffer.BlockCopy(addressBytes2, 0, shellcode, 6, 8);
            Buffer.BlockCopy(addressBytes, 0, shellcode, 46, 8);


            IntPtr allocMem = MemoryManager.VirtualAllocEx(HProcess, IntPtr.Zero, (uint)shellcode.Length, 0x1000 | 0x2000, 0x40);
            if (allocMem != IntPtr.Zero)
            {
                if (MemoryManager.WriteProcessMemory(HProcess, allocMem, shellcode, (uint)shellcode.Length, out _))
                {
                    IntPtr hThread = MemoryManager.CreateRemoteThread(HProcess, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);

                    if (hThread != IntPtr.Zero)
                    {
                        MemoryManager.WaitForSingleObject(hThread, 1000);
                        MemoryManager.CloseHandle(hThread);
                    }
                }
                MemoryManager.VirtualFreeEx(HProcess, allocMem, 0, 0x8000);
            }
        }








        /// ///////////////////////////////////////////////////////////////////////testando



        //public void TrocarSetCompleto(IntPtr hpProcess,ulong idColar, ulong idAnel1, ulong idAnel2, ulong idFlecha, ulong idOrn)
        //{
        //    if (hpProcess == IntPtr.Zero || Client.FunctionSwap == IntPtr.Zero) return;

        //    byte[] shellcode = new byte[]
        //    {
        //        0x48, 0x83, 0xEC, 0x28,                                     
                
        //        // --- ITEM 1: Ornamento (Slot 0x5) ---
        //        0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xBA, 0xB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0xFF, 0xD0,                                                 
                
        //        // --- ITEM 2: Elmo (Slot 0x1) ---
        //        0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xBA, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0xFF, 0xD0,                                                 
                
        //        // --- ITEM 3: Capa (Slot 0x3) ---
        //        0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xBA, 0xA, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0xFF, 0xD0,

        //        0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xBA, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0xFF, 0xD0,

        //        0x48, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xBA, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        //        0xFF, 0xD0,

        //        0x48, 0x83, 0xC4, 0x28,
        //        0xC3
        //    };

        //    Buffer.BlockCopy(BitConverter.GetBytes(idColar), 0, shellcode, 6, 8);
        //    Buffer.BlockCopy(BitConverter.GetBytes((ulong)Client.FunctionSwap), 0, shellcode, 26, 8);

        //    Buffer.BlockCopy(BitConverter.GetBytes(idAnel1), 0, shellcode, 38, 8);
        //    Buffer.BlockCopy(BitConverter.GetBytes((ulong)Client.FunctionSwap), 0, shellcode, 58, 8);

        //    Buffer.BlockCopy(BitConverter.GetBytes(idAnel2), 0, shellcode, 70, 8);
        //    Buffer.BlockCopy(BitConverter.GetBytes((ulong)Client.FunctionSwap), 0, shellcode, 90, 8);

        //    Buffer.BlockCopy(BitConverter.GetBytes(idFlecha), 0, shellcode, 102, 8);
        //    Buffer.BlockCopy(BitConverter.GetBytes((ulong)Client.FunctionSwap), 0, shellcode, 122, 8);

        //    Buffer.BlockCopy(BitConverter.GetBytes(idOrn), 0, shellcode, 134, 8);
        //    Buffer.BlockCopy(BitConverter.GetBytes((ulong)Client.FunctionSwap), 0, shellcode, 154, 8);

        //    IntPtr allocMem = MemoryManager.VirtualAllocEx(hpProcess, IntPtr.Zero, (uint)shellcode.Length, 0x1000 | 0x2000, 0x40);
        //    if (allocMem != IntPtr.Zero)
        //    {
        //        MemoryManager.WriteProcessMemory(hpProcess, allocMem, shellcode, (uint)shellcode.Length, out _);
        //        IntPtr hThread = MemoryManager.CreateRemoteThread(hpProcess, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);

        //        if (hThread != IntPtr.Zero)
        //        {
        //            MemoryManager.WaitForSingleObject(hThread, 1000);
        //            MemoryManager.CloseHandle(hThread);
        //        }

        //        MemoryManager.VirtualFreeEx(hpProcess, allocMem, 0, 0x8000);
        //    }
        //}


        //public void ProcessarTrocaDeSet(IntPtr hpProcess,ulong idColarAtk, ulong idAnel1Atk, ulong idAnel2Atk, ulong idFlechaAtk, ulong idOrnAtk, ulong idColarDef, ulong idAnel1Def, ulong idAnel2Def, ulong idFlechaDef, ulong idOrnDef)
        //{
        //    if (hpProcess == IntPtr.Zero) return;


        //    //int armaAtual2 = MemoryManager.ReadInt32(hpProcess, PersonagemInventario + 0xA84);
        //    //MessageBox.Show($"Arma Atual: {armaAtual2}\nArmaDef: {ArmaDef}\nArmaAtk: {ArmaAtk}", "Debug");

        //    //if (armaAtual2 == ArmaDef)
        //    //{
        //    //    TrocarSetCompleto(idColarAtk, idAnel1Atk, idAnel2Atk, idFlechaAtk, idOrnAtk);
        //    //}

        //    //if (armaAtual2 == ArmaAtk)
        //    //{
        //    //    TrocarSetCompleto(idColarDef, idAnel1Def, idAnel2Def, idFlechaDef, idOrnDef);
        //    //}
        //}


        //public static void castarSkill(ulong skillId, IntPtr hpProcess)
        //{
        //    // Converte o endereço da função para 8 bytes (Little-Endian)
        //    byte[] addressBytes = BitConverter.GetBytes(Client.SkillCall);
        //    byte[] addressBytes2 = BitConverter.GetBytes(skillId);

        //    byte[] shellcode = new byte[]
        //    {
        //        // 1. sub rsp, 0x28 (Shadow Space + Alinhamento) [4 bytes]
        //        0x48, 0x83, 0xEC, 0x28,

        //        // 2. mov rcx, 0x6a0 (1º Argumento) [10 bytes]
        //        0x48, 0xB9, 0xB0, 0x14, 0x98, 0x9B, 0x00, 0x00, 0x00, 0x00,

        //        // 3. mov rdx, 0xb (2º Argumento) [10 bytes]
        //        0x48, 0xBA, 0x85, 0x0F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

        //        // 4. mov r8, 1 (3º Argumento - 64 bits completo) [10 bytes]
        //        0x49, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

        //        // 5. mov r9, 0x15D9C8 (4º Argumento - 64 bits completo) [10 bytes]
        //        0x49, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

        //        // 6. mov rax, targetAddress (Endereço da função SkillCall) 
        //        // Inicia exatamente no índice 44 (0x2C) [2 bytes do opcode + 8 bytes de placeholder]
        //        0x48, 0xB8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,

        //        // 7. call rax [2 bytes]
        //        0xFF, 0xD0,

        //        // 8. add rsp, 0x28 (Restaura a pilha) [4 bytes]
        //        0x48, 0x83, 0xC4, 0x28,

        //        // 9. ret [1 byte]
        //        0xC3
        //    };

        //    // Soma dos tamanhos acima até o 'mov rax': 4 + 10 + 10 + 10 + 10 = 44 bytes.
        //    // O opcode '0x48, 0xB8' ocupa os 2 primeiros bytes da instrução 6, logo os dados começam no índice 46.
        //    // Vamos copiar o endereço exatamente a partir do índice 46 (0x2E).
        //    Buffer.BlockCopy(addressBytes2, 0, shellcode, 16, 8);
        //    Buffer.BlockCopy(addressBytes, 0, shellcode, 46, 8);


        //    IntPtr allocMem = MemoryManager.VirtualAllocEx(hpProcess, IntPtr.Zero, (uint)shellcode.Length, 0x1000 | 0x2000, 0x40);
        //    if (allocMem != IntPtr.Zero)
        //    {
        //        if (MemoryManager.WriteProcessMemory(hpProcess, allocMem, shellcode, (uint)shellcode.Length, out _))
        //        {
        //            IntPtr hThread = MemoryManager.CreateRemoteThread(hpProcess, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);

        //            if (hThread != IntPtr.Zero)
        //            {
        //                MemoryManager.WaitForSingleObject(hThread, 1000);
        //                MemoryManager.CloseHandle(hThread);
        //            }
        //        }
        //        MemoryManager.VirtualFreeEx(hpProcess, allocMem, 0, 0x8000);
        //    }
        //}

        public static void CancelarSkill(IntPtr BaseAddres,IntPtr hpProcess)
        {
            long targetAddress = BaseAddres.ToInt64() + 0xA6E9D0;
            byte[] addressBytes = BitConverter.GetBytes(targetAddress);

            byte[] shellcode = new byte[]
            {
                // sub rsp, 0x28 (Reserva Shadow Space + Alinhamento da Pilha)
                0x48, 0x83, 0xEC, 0x28,

                // mov rax, <targetAddress>
                0x48, 0xB8,
                addressBytes[0], addressBytes[1], addressBytes[2], addressBytes[3],
                addressBytes[4], addressBytes[5], addressBytes[6], addressBytes[7],

                // call rax
                0xFF, 0xD0,

                // add rsp, 0x28 (Restaura a Pilha)
                0x48, 0x83, 0xC4, 0x28,

                // ret
                0xC3
            };

            // 3. Alocar memória para o shellcode e executar a thread remota
            IntPtr allocMem = MemoryManager.VirtualAllocEx(hpProcess, IntPtr.Zero, (uint)shellcode.Length, 0x1000 | 0x2000, 0x40);
            if (allocMem != IntPtr.Zero)
            {
                MemoryManager.WriteProcessMemory(hpProcess, allocMem, shellcode, (uint)shellcode.Length, out _);
                IntPtr hThread = MemoryManager.CreateRemoteThread(hpProcess, IntPtr.Zero, 0, allocMem, IntPtr.Zero, 0, IntPtr.Zero);

                if (hThread != IntPtr.Zero)
                {
                    MemoryManager.WaitForSingleObject(hThread, 1000);
                    MemoryManager.CloseHandle(hThread);
                }
                MemoryManager.VirtualFreeEx(hpProcess, allocMem, 0, 0x8000);
            }
        }
    }
}
