using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovoProjetoAvalonia.Models;
using NovoProjetoAvalonia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovoProjetoAvalonia.ViewModels
{
    internal partial class ContasSalvasViewModel : ViewModelBase
    {
        private  GrupoViewModel _grupoViewModel;
        public ContasSalvasViewModel(GrupoViewModel grupoViewModel)
        {
            _grupoViewModel = grupoViewModel;
            Contas.Add(new ContaViewModel("Lenamiu", "Arqueiro", 0));
            Contas.Add(new ContaViewModel("farmzekoo1", "Guerreiro", 0));
            Contas.Add(new ContaViewModel("farmzeko2", "Feiticeira", 0));
            Contas.Add(new ContaViewModel("farmzeko3", "Arcano", 0));
            Contas.Add(new ContaViewModel("farmzeko4", "Barbaro", 0));
            Contas.Add(new ContaViewModel("farmzeko5", "Sacerdote", 0));
            Contas.Add(new ContaViewModel("farmzeko6", "Tormentador", 0));
            Contas.Add(new ContaViewModel("farmzeko7", "Mercenario", 0));
            Contas.Add(new ContaViewModel("farmzeko8", "Macaco", 0));
            Contas.Add(new ContaViewModel("farmzeko9", "Mistico", 0));

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
            Contas.Add(new ContaViewModel("teste","teste",15));

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

            Personagem novoPersonagem = new Personagem(nome, tamanhoNome,classe);
            personagens[indiceVazio] = novoPersonagem;

            StatusMensagem = $"Conta {nome} indice{indiceVazio} classe{classe} ";
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
        public void DesanexarDoJogo()
        {
            try
            {
                for (int i = 0; i < personagens.Length ;i++)
                {
                    if (personagens[i] != null)
                    {
                        var p = personagens[i];
                        if (p != null)
                        {
                            IntPtr hProcess = p.getHProcess();
                            IntPtr sharedAllocMem = p.getMemorySpace();

                            // 1. Libera a memória alocada (VirtualAllocEx) se ela existir
                            if (hProcess != IntPtr.Zero && sharedAllocMem != IntPtr.Zero)
                            {
                                // 0x8000 (MEM_RELEASE) libera o bloco alocado de volta para o sistema operacional do jogo
                                MemoryManager.VirtualFreeEx(hProcess, sharedAllocMem, 0, 0x8000);
                            }

                            // 2. Fecha o Handle aberto do processo do jogo
                            if (hProcess != IntPtr.Zero)
                            {
                                MemoryManager.CloseHandle(hProcess);
                            }

                            // 3. Reseta os valores DIRETAMENTE no objeto do personagem 
                            // (Usando métodos setters equivalentes aos seus getters):
                            p.setHProcess(IntPtr.Zero);
                            p.setMemorySpace(IntPtr.Zero);
                            p.setProcess(null);
                        }
                    }

                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar log de erro se necessário
                System.Diagnostics.Debug.WriteLine($"Erro ao desanexar: {ex.Message}");
            }
        }


        [RelayCommand]
        private async Task CancelarSkill()
        {
            
            grupo.skillSender(personagens[0], 0x119c);

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