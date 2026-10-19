using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovoProjetoAvalonia.Models;
using NovoProjetoAvalonia.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System;

namespace NovoProjetoAvalonia.ViewModels
{
    internal partial class ContasSalvasViewModel : ViewModelBase
    {
        private  GrupoViewModel _grupoViewModel;
        public ContasSalvasViewModel(GrupoViewModel grupoViewModel)
        {
            _grupoViewModel = grupoViewModel;
            
        }

        

        public static ObservableCollection<ContaViewModel> Contas { get; } = new();
        public static Personagem [] personagens = new Personagem[10];

        public static GrupoManagement grupo = new GrupoManagement();

       

        // Com a inicial maiúscula para coincidir com o Text="{Binding StatusMensagem}"
        [ObservableProperty]
        private string statusMensagem = string.Empty;

        [RelayCommand]
        private void AdicionarConta() // 'A' maiúsculo
        {
            Contas.Add(new ContaViewModel());

        }

        
        
        [RelayCommand]
        private void RemoverConta(ContaViewModel conta) // 'R' maiúsculo
        {
            if (conta.Status == true)
            {
                conta.Status = false;
                personagens[conta.Indice].desanexar();

            }

            personagens[conta.Indice] = null;
            StatusMensagem = $"Conta de {conta.NomePersonagem} salva, indice {conta.Indice} ";

            // 1. Remove da lista principal de contas salvas
            if (Contas.Contains(conta))
                Contas.Remove(conta);

            // 2. Remove da lista de contas logadas (usada na página de grupos)
            if (_grupoViewModel?.ContasLogadas != null && _grupoViewModel.ContasLogadas.Contains(conta))
                _grupoViewModel.ContasLogadas.Remove(conta);

            // 3. Remove da box de pessoas dentro do grupo
            if (_grupoViewModel?.ContasNoGrupo != null && _grupoViewModel.ContasNoGrupo.Contains(conta))
                _grupoViewModel.ContasNoGrupo.Remove(conta);
            

        }

        [RelayCommand]
        private void SalvarConta(ContaViewModel conta) // 'S' maiúsculo
        {

            int indiceVazio = 0; 

            for (int i = 0; i < personagens.Length; i++)
            {
                if (personagens[i] == null)
                {
                    indiceVazio = i;
                    break;
                }
            }

            conta.Indice = indiceVazio;
            string login = conta.Login;
            string senha = conta.Senha;
            string nome = conta.NomePersonagem;
            string classe = conta.Classe;
            int tamanhoNome = conta.NomePersonagem.Length;

            Personagem novoPersonagem = new Personagem(nome, tamanhoNome);
            personagens[indiceVazio] = novoPersonagem;

            StatusMensagem = $"Conta de {nome} salva, indice {indiceVazio} ";
        }

            
        

        [RelayCommand]
        private async Task Jogar(ContaViewModel contaClicada)
        {
            if (contaClicada == null) return;

            // 1. Descobrimos o índice exato da caixinha na lista!
            int indice = Contas.IndexOf(contaClicada);

            _ = Task.Run(async () =>
                {
                    await personagens[indice].AnexarProcessoAsync(nomeLido =>
                    {
                        Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                        {
                            NomeDebug = nomeLido;
                            
                        });
                    });
                });
                Contas[indice].Status = true;
            }

        [RelayCommand]
        private async Task CancelarSkill()
        {
            byte[] id = BitConverter.GetBytes(0x2354);
            grupo.teleporte(personagens[0], 0x2354, 0x02);

        }

        [RelayCommand]
        
        private void Hook()
        {
            MemoryManager.Nops(personagens[0].getHProcess(), personagens[0].getBaseAddress(), 18);

        }

        
        //private void CancelarSkill()
        //{
        //    long[] skills = new long[] { 0xf85, 0xf77, 0xf7c };
        //    foreach (long skill in skills)
        //    {
        //        grupo.castar(personagens[0], skill);

        //        while (personagens[0].getCasting() != 1)
        //        {

        //        }

        //        while (personagens[0].getCasting() == 1)
        //        {

        //        }
        //    }

        //}

        [ObservableProperty]
        public static string nomeDebug = "Aguardando leitura...";

    }
}