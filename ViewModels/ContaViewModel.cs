using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovoProjetoAvalonia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NovoProjetoAvalonia.ViewModels
{
    public partial class ContaViewModel: ObservableObject
    {
        [ObservableProperty]
        private string login = string.Empty;

        [ObservableProperty]
        private string senha = string.Empty;

        [ObservableProperty]
        private string nomePersonagem = string.Empty;
        
        [ObservableProperty]
        protected bool status = false;

        [ObservableProperty]
        protected int indice = 0;

        [ObservableProperty]
        protected String classe = string.Empty;

        [ObservableProperty]
        protected List<Habilidades> comboArea;

        [ObservableProperty]
        protected List<Habilidades> comboSingle;
    }
}
