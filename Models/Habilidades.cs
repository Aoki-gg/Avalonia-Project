using System;
using System.Collections.Generic;
using System.Text;

namespace NovoProjetoAvalonia.Models
{
    public class Habilidades
    {
        public String classe;
        
        List<long> habilidades;

        public Habilidades(String classe)
        {
           this.habilidades = new List<long>();
            this.classe = classe;
        }


        public List<long> getHabilidades()
        {
            return this.habilidades;
        }

        public void adicionarHabilidade(long id)
        {
            this.habilidades.Add(id);
        }

        public void removerHabilidade(long id)
        {
            foreach (long habilidade in habilidades)
            {
                if (habilidade == id)
                {
                    this.habilidades.Remove(habilidade);
                }
            }
        }

        public void limparHabilidades()
        {
            this.habilidades.Clear();
        }

        public String ToString()
        {
            String macros = "";
            
            foreach (long habili in habilidades)
            {
                macros += habili.ToString();
            }

            return macros;
        }
    }
}
