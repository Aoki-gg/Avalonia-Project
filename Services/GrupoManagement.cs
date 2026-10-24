using Avalonia.Controls;
using NovoProjetoAvalonia.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NovoProjetoAvalonia.Services
{
    internal class GrupoManagement
    {

        public GrupoManagement()
        {

        } 

        public void mandarGrupo(Personagem personagem, byte[] id)
        {
            InternFunctions.mandarGrupo(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getFuncaoMandarGrupo(), id);
        }

        public void kickarGrupo(Personagem personagem, byte[] id)
        {
            InternFunctions.kickarGrupo(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getFuncaoKickarGrupo(), id);
        }

        public void passarLider(Personagem personagem, byte[] id)
        {
            InternFunctions.passarLider(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getFuncaoPassarLider(), id);
        }

        public void sairDoGrupo(Personagem personagem)
        {
            InternFunctions.sairGrupo(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getFuncaoSairGrupo());
        }

        public void pegarTarget(Personagem personagem, byte[] id)
        {
            InternFunctions.pegarTarget(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getFuncaoTarget(), id);
        }

        public void seguir(Personagem personagem, byte[] idLider) //done
        {
            InternFunctions.seguir(personagem.getHProcess(),personagem.getMemorySpace(), personagem.getHostPlayerBytes() ,personagem.getFuncaoSeguir(), idLider);

        }

        public void aceitarGrupo(Personagem personagem, byte[] id)
        {
            InternFunctions.aceitar(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getParty(), personagem.getFuncaoAceitar(), id);
        }

        public void castar(Personagem personagem, long skillId)
        {
            InternFunctions.Castar(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getFuncaoCastar(), personagem.getHostPlayerBytes(), skillId); 
        }

        public void teleporte(Personagem personagem, long idLocal, long slotItem)
        {
            InternFunctions.teleporte(personagem.getHProcess(), personagem.getMemorySpace(),personagem.getBuffer(), personagem.getPacketStruct(), personagem.getPacketSender(), idLocal, slotItem);
        }

        public void moveTo(Personagem personagem, float[] coords)
        {
            InternFunctions.moveTo(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getBuffer(), personagem.getFuncaoMoveTo(), personagem.getHostPlayerBytes(), coords);
        }

        public void pegarMissao(Personagem personagem, long idNpc)
        {
            InternFunctions.pegarMissao(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getBuffer(), personagem.getBaseAddress(), personagem.getPacketStruct(), personagem.getPacketSender(),  idNpc);
        }

        public void skillSender(Personagem personagem, long skillId)
        {
            InternFunctions.skillSender(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getBuffer(), personagem.getHostPlayer(), personagem.getPacketStruct(), personagem.getPacketSender(), skillId);
        }

        public void skillSenderFox(Personagem personagem, long skillId)
        {
            InternFunctions.skillSenderFox(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getBuffer(), personagem.getHostPlayer(), personagem.getPacketStruct(), personagem.getPacketSender(), skillId);
        }

        public void daimonSender(Personagem personagem)
        {
            InternFunctions.daimonSender(personagem.getHProcess(), personagem.getMemorySpace(), personagem.getBuffer(), personagem.getHostPlayer(), personagem.getPacketStruct(), personagem.getPacketSender());
        }

        public void teste(Personagem personagem, long npcid)
        {
            InternFunctions.telarseminterface(personagem.getHProcess(), personagem.getBaseAddress(), personagem.getPacketStruct(), personagem.getPacketSender(), npcid);
        }







        //public void pegarMissao(Personagem personagem, long idMissao)
        //{
        //    personagem.pegarMissao(idMissao);
        //}

        //public void teleportar(Personagem personagem, long ponteiroDinamico, long ponto)
        //{
        //    personagem.teleportar(ponteiroDinamico, ponto);
        //}






    }
}
