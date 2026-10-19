using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using NovoProjetoAvalonia.ViewModels;

namespace NovoProjetoAvalonia.Views;

public partial class GrupoView : UserControl
{
    private static readonly DataFormat<ContaViewModel> ContaFormat =
        DataFormat.CreateInProcessFormat<ContaViewModel>("conta-item");

    public GrupoView()
    {
        InitializeComponent();
    }

    private async void Border_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is ContaViewModel conta)
        {
            var dataTransfer = new DataTransfer();
            dataTransfer.Add(DataTransferItem.Create(ContaFormat, conta));

            await DragDrop.DoDragDropAsync(e, dataTransfer, DragDropEffects.Move);
        }
    }

    // --- LÓGICA DA CAIXA (DROP ZONE) ---
    private void DropZone_DragOver(object? sender, DragEventArgs e)
    {
        if (e.DataTransfer.Contains(ContaFormat))
        {
            e.DragEffects = DragDropEffects.Move;
        }
        else
        {
            e.DragEffects = DragDropEffects.None;
        }
    }

    private void DropZone_Drop(object? sender, DragEventArgs e)
    {
        if (e.DataTransfer.TryGetValue(ContaFormat) is ContaViewModel draggedItem)
        {
            if (this.DataContext is GrupoViewModel vm)
            {
                // Remove da lista principal se estiver lá
                if (vm.ContasLogadas.Contains(draggedItem))
                {
                    vm.ContasLogadas.Remove(draggedItem);
                }

                // Adiciona na caixa se ainda não estiver lá
                if (!vm.ContasNoGrupo.Contains(draggedItem))
                {
                    vm.ContasNoGrupo.Add(draggedItem);
                }
            }
        }
    }

    // --- LÓGICA DA LISTA PRINCIPAL (INVERSO) ---
    private void ScrollViewer_DragOver(object? sender, DragEventArgs e)
    {
        if (e.DataTransfer.Contains(ContaFormat))
        {
            e.DragEffects = DragDropEffects.Move;
        }
        else
        {
            e.DragEffects = DragDropEffects.None;
        }
    }

    private void ScrollViewer_Drop(object? sender, DragEventArgs e)
    {
        if (e.DataTransfer.TryGetValue(ContaFormat) is ContaViewModel draggedItem)
        {
            if (this.DataContext is GrupoViewModel vm)
            {
                // Se o item estava na caixa, remove de lá
                if (vm.ContasNoGrupo.Contains(draggedItem))
                {
                    vm.ContasNoGrupo.Remove(draggedItem);
                    

                }

                // Descobre se foi solto em cima de um item específico para reordenar, ou apenas adiciona no fim
                var sourceControl = e.Source as Avalonia.Visual;
                var targetBorder = sourceControl?.FindAncestorOfType<Border>();
                var targetItem = targetBorder?.DataContext as ContaViewModel;

                if (targetItem != null && !draggedItem.Equals(targetItem))
                {
                    int oldIndex = vm.ContasLogadas.IndexOf(draggedItem);
                    int newIndex = vm.ContasLogadas.IndexOf(targetItem);

                    if (oldIndex >= 0 && newIndex >= 0)
                    {
                        vm.ContasLogadas.Move(oldIndex, newIndex);
                    }
                    else if (!vm.ContasLogadas.Contains(draggedItem))
                    {
                        vm.ContasLogadas.Add(draggedItem);
                    }
                }
                else
                {
                    // Se soltou no espaço vazio da lista principal, apenas garante que está lá
                    if (!vm.ContasLogadas.Contains(draggedItem))
                    {
                        vm.ContasLogadas.Add(draggedItem);
                    }
                }
            }
        }
    }
}