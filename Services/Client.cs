using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NovoProjetoAvalonia.Services
{
    internal class Client
    {
        


        public static IntPtr HProcess { get; private set; } = IntPtr.Zero;
        public static IntPtr BaseAddress { get; private set; } = IntPtr.Zero;
        public static IntPtr FunctionSwap { get; private set; } = IntPtr.Zero;
        public static IntPtr SkillCall { get; private set; } = IntPtr.Zero;
        public static IntPtr SkillCall2 { get; private set; } = IntPtr.Zero;
        public static IntPtr baseIsCastingSkillAddress { get; private set; } = IntPtr.Zero;
        public static IntPtr isCastingSkillAddress { get; private set; } = IntPtr.Zero;
        public static IntPtr PersonagemRepositorio { get; private set; } = IntPtr.Zero;
        public static IntPtr PersonagemInventario { get; private set; } = IntPtr.Zero;
        public static IntPtr InimigoItems { get; private set; } = IntPtr.Zero;

        public Process processoSelecionado;

        private CancellationTokenSource? _cts;
        private bool _verificacaoAtiva = false;

        public bool Conectar(Process processo)
        {

            HProcess = MemoryManager.OpenProcess(0x001F0FFF, false, processo.Id);

            if (HProcess == IntPtr.Zero) return false;

            BaseAddress = processo.MainModule.BaseAddress;
            FunctionSwap = IntPtr.Add(BaseAddress, 0xA7F8D0);
            SkillCall = IntPtr.Add(BaseAddress, 0x8b4880);
            SkillCall2 = IntPtr.Add(BaseAddress, 0x8DFCD0);
            PersonagemRepositorio = (IntPtr)MemoryManager.ReadInt64(HProcess,(IntPtr)MemoryManager.ReadInt64(HProcess, BaseAddress + 0x01733D70) + 0x10);
            PersonagemInventario = (IntPtr)MemoryManager.ReadInt64(HProcess, (IntPtr)MemoryManager.ReadInt64(HProcess, BaseAddress + 0x01734248) + 0x60);
            baseIsCastingSkillAddress = (IntPtr)MemoryManager.ReadInt64(HProcess, (IntPtr)MemoryManager.ReadInt64(HProcess, BaseAddress + 0x0172CEA0) + 0x98);
            isCastingSkillAddress = baseIsCastingSkillAddress + 0x138 + 0x5DC;
            return true;

        }

        public void DesconectarEParar()
        {
            _cts?.Cancel();
            _verificacaoAtiva = false;
        }

    }
}
