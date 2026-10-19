using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovoProjetoAvalonia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Avalonia.Collections;

namespace NovoProjetoAvalonia.ViewModels
{
    internal partial class MacrosViewModel : ViewModelBase
    {

    //    public AvaloniaDictionary<String, int> guerreiro = new AvaloniaDictionary<String, int>()
    //    {
    //        {"Aura Evil ", 423 },
    //        {"Aura God ", 422 },
    //        {"Ira Evil", 413 },
    //        {"Ira God", 412 },
    //        {"Ira Fusao", 3931 },

    //        {"Rugido God ", 424 },
    //        {"Rugido Evil ", 425 },

    //        {"Chi3 God ", 362 },
    //        {"Chi3 Evil ", 363 },

    //    };
    //public AvaloniaDictionary<String, int> mago = new AvaloniaDictionary<String, int>()
    //    {
    //        {"Marca de Fogo God ", 440 },
    //        {"Marca de Fogo Evil", 441 },
    //        {"Marca de Fogo Evil", 3654 },
    //        {"Lanterna de Chamas God", 444 },
    //        {"Lanterna de Chamas Evil", 445 },
    //        {"Lanterna de Chamas Fusao", 6440 },
    //        {"Marca da Chama Divina God", 446 },
    //        {"Marca da Chama Divina Evil", 447 },
    //        {"Marca da Chama Divina Fusao", 4376 },
    //        {"79 ", 0x386 },

    //        {"Chi3 God ", 0x16C },
    //        {"Chi3 Evil ", 0x16D },
    //    };
    //public AvaloniaDictionary<String, int> barbaro = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> feiticeira = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> arqueiro = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> sacerdote = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> mistico = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> arcano = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> espiritualista = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> mercenario = new AvaloniaDictionary<String, int>()
    //{

    //};
    //public AvaloniaDictionary<String, int> tormentador = new AvaloniaDictionary<String, int>()
    //{

    //};

    public List<Habilidades> comboArea { get; set; }
    public List<Habilidades> comboSingle { get; set; }

    public MacrosViewModel()
    {
        //this.comboArea = new List<Habilidades>();
        //this.comboArea.Add(new Habilidades("Barbaro"));
        //this.comboArea.Add(new Habilidades("Feiticeira"));
        //this.comboArea.Add(new Habilidades("Sacerdote"));
        //this.comboArea.Add(new Habilidades("Arqueiro"));
        //this.comboArea.Add(new Habilidades("Mistico"));
        //this.comboArea.Add(new Habilidades("Arcano"));
        //this.comboArea.Add(new Habilidades("Mago"));
        //this.comboArea.Add(new Habilidades("Guerreiro"));
        //this.comboArea.Add(new Habilidades("Retalhador"));
        //this.comboArea.Add(new Habilidades("Atiradora"));
        //this.comboArea.Add(new Habilidades("Paladino"));
        //this.comboArea.Add(new Habilidades("Bardo"));
        //this.comboArea.Add(new Habilidades("Andarilho"));
        //this.comboArea.Add(new Habilidades("Mercenario"));
        //this.comboArea.Add(new Habilidades("Espiritualista"));
        //this.comboArea.Add(new Habilidades("Tormentador"));
        //this.comboArea.Add(new Habilidades("Ceifador"));


        //this.comboSingle = new List<Habilidades>();
        //this.comboSingle.Add(new Habilidades("Barbaro"));
        //this.comboSingle.Add(new Habilidades("Feiticeira"));
        //this.comboSingle.Add(new Habilidades("Sacerdote"));
        //this.comboSingle.Add(new Habilidades("Arqueiro"));
        //this.comboSingle.Add(new Habilidades("Mistico"));
        //this.comboSingle.Add(new Habilidades("Arcano"));
        //this.comboSingle.Add(new Habilidades("Mago"));
        //this.comboSingle.Add(new Habilidades("Guerreiro"));
        //this.comboSingle.Add(new Habilidades("Retalhador"));
        //this.comboSingle.Add(new Habilidades("Atiradora"));
        //this.comboSingle.Add(new Habilidades("Paladino"));
        //this.comboSingle.Add(new Habilidades("Bardo"));
        //this.comboSingle.Add(new Habilidades("Andarilho"));
        //this.comboSingle.Add(new Habilidades("Mercenario"));
        //this.comboSingle.Add(new Habilidades("Espiritualista"));
        //this.comboSingle.Add(new Habilidades("Tormentador"));
        //this.comboSingle.Add(new Habilidades("Ceifador"));
    }

    [RelayCommand]
    public void AdicionarComboArea(String nome)
    {
        foreach (Habilidades combo in comboArea)
        {
            if (combo.classe.Equals(nome))
            {
                combo.adicionarHabilidade(0x23A);
            }
        }
    }

    [ObservableProperty]
    public static string lista = "Aguardando leitura...";

    [RelayCommand]
    public void Listar()
    {
        StringBuilder sb = new StringBuilder();

        foreach (Habilidades combo in comboArea)
        {
            // Certifique-se de usar a propriedade que deseja mostrar (ex: combo.classe)
            string valores = string.Join(", ", combo.getHabilidades());
            sb.AppendLine($"{combo.classe}: [ {valores} ]");
        }

        // Atualiza a propriedade observável para refletir no ecrã
        Lista = sb.ToString();
    }
}
}
