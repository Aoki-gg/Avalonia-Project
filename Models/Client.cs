using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NovoProjetoAvalonia.Services;

namespace NovoProjetoAvalonia.Models
{
    internal class Client
    {
        protected Process processoAnexado { get; set; }
        protected IntPtr HProcess { get; set; } 
        protected IntPtr BaseAddress { get; set; }
        protected IntPtr HostPlayer { get; set; }

        protected IntPtr isCasting { get; set; }
        protected long party { get; set; }

        //Pre-carregamento de funcoes para ter melhor perfomance.

        protected byte[] packetSender { get; set; }

        protected byte[] packetStruct { get; set; }
        protected byte[] HostPlayerBytes { get; set; }
        protected byte[] funcaoSeguir { get; set; }

        protected byte[] funcaoAceitar { get; set; }

        protected byte[] funcaoMandarGrupo { get; set; }

        protected byte[] funcaoKickarGrupo { get; set; }

        protected byte[] funcaoPassarLider { get; set; }

        protected byte[] funcaoSairGrupo { get; set; }

        protected byte[] funcaoTarget { get; set; }

        protected byte[] funcaoCastar { get; set; }

        protected byte[] funcaoTeleporte { get; set; }

        protected byte[] funcaoMoveTo { get; set; }

        protected byte[] funcaoPegarMissao { get; set; }




    }
}
