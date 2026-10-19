using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace NovoProjetoAvalonia.Services
{
    public class MemoryManager
    {

        [DllImport("kernel32.dll", SetLastError = true)] public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);
        //[DllImport("kernel32.dll")] public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        //[DllImport("kernel32.dll")] public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out IntPtr lpNumberOfBytesWritten);
        [DllImport("kernel32.dll")] public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);
        [DllImport("kernel32.dll")] public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("kernel32.dll")] public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);
        [DllImport("kernel32.dll")] public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);
        [DllImport("kernel32.dll", SetLastError = true)] public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        [DllImport("kernel32.dll")] public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll")] public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);
        const uint MEM_COMMIT = 0x1000;
        const uint MEM_RESERVE = 0x2000;
        const uint PAGE_EXECUTE_READWRITE = 0x40;
        const uint MEM_RELEASE = 0x8000;


        public static int ReadInt32(IntPtr HProcess,IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(HProcess, address, buffer, 4, out _);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static long ReadInt64(IntPtr HProcess, IntPtr address)
        {
            byte[] buffer = new byte[8];
            ReadProcessMemory(HProcess, address, buffer, 8, out _);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static byte ReadByte(IntPtr HProcess, IntPtr address)
        {
            byte[] buffer = new byte[1];
            ReadProcessMemory(HProcess, address, buffer, 1, out _);
            return buffer[0];
        }


        public static byte[] ReadBytes(IntPtr HProcess,IntPtr address, int length)
        {
            byte[] buffer = new byte[length];

            ReadProcessMemory(HProcess, address, buffer, length, out int lidos);

            return buffer;
        }

        // Leitura de String Unicode (UTF-16)
        public static string ReadUnicodeString(IntPtr HProcess,IntPtr address, int length = 32)
        {
            byte[] bytes = ReadBytes(HProcess, address, length * 2);
            string texto = Encoding.Unicode.GetString(bytes);
            int nullIndex = texto.IndexOf('\0');
            if (nullIndex >= 0) texto = texto.Substring(0, nullIndex);
            return texto.Trim();
        }

        public static float ReadFloat(IntPtr HProcess ,IntPtr address)
        {
            byte[] buffer = new byte[4];
            ReadProcessMemory(HProcess, address, buffer, 4, out _);
            return BitConverter.ToSingle(buffer, 0);
        }



        public static void Nops(IntPtr hProcess, IntPtr baseAddress, int totalBytes)
        {
            // 1. Calcula o endereço final somando o offset
            IntPtr targetAddress = IntPtr.Add(baseAddress, 0xacf56c);

            uint oldProtect;

            // 2. Altera a proteção da memória no processo de destino usando VirtualProtectEx
            // (Nota: para outro processo, o correto é VirtualProtectEx)
            if (!VirtualProtectEx(hProcess, targetAddress, (uint)totalBytes, PAGE_EXECUTE_READWRITE, out oldProtect))
            {
                return;
            }

            // 3. Cria um array preenchido com 0x90 (NOP)
            byte[] nopBuffer = new byte[totalBytes];
            for (int i = 0; i < totalBytes; i++)
            {
                nopBuffer[i] = 0x90;
            }

            // 4. Escreve os NOPs na memória do processo alvo
            WriteProcessMemory(hProcess, targetAddress, nopBuffer, (uint)totalBytes, out _);

            // 5. Restaura a proteção antiga da memória
            VirtualProtectEx(hProcess, targetAddress, (uint)totalBytes, oldProtect, out _);
        }
    }
}
