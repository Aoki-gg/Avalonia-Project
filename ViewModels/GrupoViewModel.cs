using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovoProjetoAvalonia.Models;
using NovoProjetoAvalonia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NovoProjetoAvalonia.ViewModels
{
    internal partial class GrupoViewModel : ViewModelBase
    {

        private ContasSalvasViewModel _contasViewModel;
        public GrupoViewModel(ContasSalvasViewModel contasSalvasViewModel)
        {
            _contasViewModel = contasSalvasViewModel;
        }

        protected List<Personagem> personagens = new List<Personagem>();

      
        public ObservableCollection<ContaViewModel> ContasLogadas { get; } = new();
        public ObservableCollection<ContaViewModel> ContasNoGrupo { get; } = new();

        private GrupoManagement grupo = new GrupoManagement();

        private long idLider = 0x41440;

        [ObservableProperty]
        private string statusMensagem = string.Empty;


        public void RemoverContaDoGrupoBox(ContaViewModel conta)
        {
            ContasNoGrupo.Remove(conta);
        }


        public void PersonagensDisponiveis()
        {
            ContasLogadas.Clear();

            foreach (ContaViewModel conta in ContasSalvasViewModel.Contas)
            {
                if (conta.Status == true)
                {
                    ContasLogadas.Add(conta);
                }
            }

            StatusMensagem = $"Contas em grupo {ContasNoGrupo.Count} /n Contas Logadas {ContasLogadas.Count}";


        }

        [RelayCommand]
        public async Task CriarGrupo()
        {

            byte[] idlider = personagens[0].getIdBytes();
            if (personagens.Count != 0)
            {
                personagens.Clear();
            }

            foreach (ContaViewModel conta in ContasNoGrupo)
            {
                if (conta.Status == true)
                {
                    personagens.Add(ContasSalvasViewModel.personagens[conta.Indice]);
                    ///logica de anexar
                }
            }

            Personagem lider = personagens[0];

            foreach (Personagem personagem in personagens)
            {
                
                grupo.mandarGrupo(lider, personagem.getIdBytes());

            }

            await Task.Delay(1000);

            foreach (Personagem personagem in personagens)
            {
                await Task.Delay(200);
                grupo.aceitarGrupo(personagem, idlider);

            }
        }

        [RelayCommand]
        public async Task DesfazerGrupo()
        {
            grupo.passarLider(personagens[0], personagens[1].getIdBytes());

            await Task.Delay(500);

            foreach (Personagem personagem in personagens)
            {
                grupo.sairDoGrupo(personagem);
                ///logica de desanexar
                await Task.Delay(100);
            }

            personagens.Clear();

        }

        [RelayCommand]
        private async Task RemontarGrupo()
        {
            Personagem lider = personagens[0];

            grupo.passarLider(lider, personagens[1].getIdBytes());

            await Task.Delay(500);

            foreach (Personagem personagem in personagens)
            {
                grupo.sairDoGrupo(personagem);
                await Task.Delay(100);
            }

            await Task.Delay(1000);

            foreach (Personagem personagem in personagens)
            {
                grupo.mandarGrupo(lider, personagem.getIdBytes());
                await Task.Delay(100);
            }

            await Task.Delay(1000);

            foreach (Personagem personagem in personagens)
            {
                grupo.aceitarGrupo(personagem, lider.getIdBytes());
                await Task.Delay(100);
            }

        }

        [RelayCommand]
        public void SeguirLider()
        {
            Personagem lider = personagens[0];
            foreach (Personagem personagem in personagens)
            {
                if (personagem.getId() != lider.getId())
                {
                    grupo.seguir(personagem, lider.getIdBytes());
                }
            }
        }

        [RelayCommand]
        public void PegarTargetLider()
        {


            foreach (Personagem personagem in personagens)

            {
                if (personagem.getId() != personagens[0].getId())
                {
                    grupo.pegarTarget(personagem, personagens[0].getIdBytes());
                }
            }
        }

        [RelayCommand]
        public void MoverGrupo()
        {
            float[] coords = new float[] { 1630f, 219f, 829f };

            foreach (Personagem personagem in personagens)
            {
                grupo.moveTo(personagem, coords);
            }

        }
        [RelayCommand]
        public async Task TeleportarGrupo()
        {
            long lugar = 0x0ED9;

            foreach (Personagem personagem in personagens)
            {
                grupo.teleporte(personagem, lugar, 0);
            }
        }

        [RelayCommand]
        public async Task AbrirNpcGrupo()
        {

        }

        [RelayCommand]
        public async Task PegarMissaoGrupo()
        {

        }

        [RelayCommand]
        public async Task ComboArea()
        {

        }

        [RelayCommand]
        public async Task ComboSingle()
        {

        }

        public async Task ComboBoss()
        {

        }
    }
}