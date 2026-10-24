using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HarfBuzzSharp;
using NovoProjetoAvalonia.Models;
using NovoProjetoAvalonia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NovoProjetoAvalonia.ViewModels
{
    internal partial class GrupoViewModel : ViewModelBase
    {

        private ContasSalvasViewModel _contasViewModel;


        public GrupoViewModel()
        {
            GlobalKeyboardHook.KeyPressed += OnAtalhoGlobalPressionado;
            GlobalKeyboardHook.KeyPressed += OnAtalhoGlobalPressionado2;
            GlobalKeyboardHook.KeyPressed += OnAtalhoGlobalPressionado3;
            GlobalKeyboardHook.KeyPressed += OnAtalhoGlobalPressionado4;
        }

        public GrupoViewModel(ContasSalvasViewModel contasSalvasViewModel) : this()
        {
            _contasViewModel = contasSalvasViewModel;
        }

        private void OnAtalhoGlobalPressionado(object? sender, int vkCode)
        {

            if (vkCode == 0x70)
            {
                Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    SeguirLider();
                });
            }
        }

        private void OnAtalhoGlobalPressionado2(object? sender, int vkCode)
        {

            if (vkCode == 0x71)
            {
                Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    combarSemTM();
                });
            }
        }

        private void OnAtalhoGlobalPressionado3(object? sender, int vkCode)
        {

            if (vkCode == 0x72)
            {
                Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    combarBoss();
                });
            }
        }

        private void OnAtalhoGlobalPressionado4(object? sender, int vkCode)
        {

            if (vkCode == 0x73)
            {
                Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(() =>
                {
                    buffar();
                });
            }
        }

        protected List<Personagem> personagens = new List<Personagem>();

        public ObservableCollection<ContaViewModel> ContasLogadas { get; } = new();
        public ObservableCollection<ContaViewModel> ContasNoGrupo { get; } = new();

        private GrupoManagement grupo = new GrupoManagement();

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
            byte[] idlider = personagens[0].getIdBytes();

            foreach (Personagem personagem in personagens)
            {
                
                grupo.mandarGrupo(lider, personagem.getIdBytes());

            }

            await Task.Delay(1000);

            foreach (Personagem personagem in personagens)
            {
                
                grupo.aceitarGrupo(personagem, idlider);
                await Task.Delay(100);
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
        public async Task combarSemTM()
        {
            byte[] target = personagens[0].getIdBytes();
            long targ = personagens[0].getTarget();

            var tarefas = personagens.Select(async personagem =>
            {
                string classe = personagem.getClasse();



                if (classe.Equals("Feiticeira"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x7C2);
                    await Task.Delay(100);
                    grupo.skillSender(personagem, 0xEAF);
                    await Task.Delay(1000);
                    grupo.skillSender(personagem, 0x7C2);

                }

                else if (classe.Equals("Guerreiro"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0xF5C);
                    await Task.Delay(100);
                    grupo.daimonSender(personagem);
                }

                else if (classe.Equals("Mercenario"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x4F4);
                }

                else if (classe.Equals("Arcano"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x1152);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x8C9);
                    await Task.Delay(300);

                }


                else if (classe.Equals("Tormentador"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0xA46);

                }

                else if (classe.Equals("Sacerdote"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x18A1);
                }

                else if (classe.Equals("Mistico"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x67C);
                }


            });

            // Aguarda todas as tarefas terminarem
            await Task.WhenAll(tarefas);
        }

        [RelayCommand]
        public async Task combarBoss()
        {
            byte[] target = personagens[0].getIdBytes();
            long targ = personagens[0].getTarget();

            var tarefas = personagens.Select(async personagem =>
            {
                string classe = personagem.getClasse();

                if (classe.Equals("Feiticeira"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x7C2);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0xEAF);
                    await Task.Delay(2000);
                    grupo.skillSenderFox(personagem, 0x119C);
                    await Task.Delay(1000);
                    grupo.skillSender(personagem, 0x7C2);

                }

                else if (classe.Equals("Guerreiro"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0xF5C);
                    await Task.Delay(100);
                    grupo.daimonSender(personagem);
                }
                else if (classe.Equals("Mercenario"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x4F4);
                }

                else if (classe.Equals("Arcano"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x1152);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x8C9);
                    await Task.Delay(300);

                }


                else if (classe.Equals("Tormentador"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0xCD1);
                }

                else if (classe.Equals("Sacerdote"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x18A1);
                }

                else if (classe.Equals("Mistico"))
                {
                    grupo.pegarTarget(personagem, target);
                    await Task.Delay(300);
                    grupo.skillSender(personagem, 0x67C);
                }
            });

            // Aguarda todas as tarefas terminarem
            await Task.WhenAll(tarefas);
        }


        [RelayCommand]
        public void buffar()
        {
            foreach (Personagem personagem in personagens)
            {

                String classe = personagem.getClasse();

                if (classe.Equals("Macaco"))
                {
                    grupo.skillSender(personagem, 0x1980);
                }

                else if (classe.Equals("Arcano"))
                {
                    grupo.skillSender(personagem, 0x1867);
                }

                else if (classe.Equals("Feiticeira"))
                {
                    grupo.skillSender(personagem, 0x288);
                }

                else if (classe.Equals("Guerreiro"))
                {
                    grupo.skillSender(personagem, 0x1A6);
                }

                else if (classe.Equals("Sacerdote"))
                {
                    grupo.skillSender(personagem, 0x8AE);
                }

                else if (classe.Equals("Barbaro"))
                {
                    grupo.skillSender(personagem, 0x1B57);
                    grupo.skillSender(personagem, 0x200);
                }

                else if (classe.Equals("Mistico"))
                {
                    grupo.skillSender(personagem, 0x73D);
                    grupo.skillSender(personagem, 0x744);
                }

                else if (classe.Equals("Tormentador"))
                {
                    grupo.skillSender(personagem, 0xAF1);
                }

                else if (classe.Equals("Mercenario"))
                {
                    grupo.skillSender(personagem, 0x4FC);
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