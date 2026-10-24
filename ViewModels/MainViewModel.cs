using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NovoProjetoAvalonia.ViewModels;
using NovoProjetoAvalonia.Services;

namespace NovoProjetoAvalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly InicioViewModel _inicioViewModel;
    private readonly RepositorioViewModel _repositorioViewModel;
    private readonly ContasSalvasViewModel _contasViewModel;
    private readonly GrupoViewModel _gruposViewModel;
    private readonly MacrosViewModel _macrosViewModel;

    [ObservableProperty]
    private ViewModelBase _paginaAtual;

    public MainViewModel()
    {

        _inicioViewModel = new InicioViewModel();
        _repositorioViewModel = new RepositorioViewModel();
        _contasViewModel = new ContasSalvasViewModel(_gruposViewModel);
        _gruposViewModel = new GrupoViewModel(_contasViewModel);
        _macrosViewModel = new MacrosViewModel();

        _paginaAtual = new InicioViewModel();
    }

    [RelayCommand]
    private void IrParaInicio()
    {
        PaginaAtual = _inicioViewModel;
    }

    [RelayCommand]
    private void IrParaRepositorio()
    {
        PaginaAtual = _repositorioViewModel; 
    }
    
    [RelayCommand]
    private void IrParaContas()
    {
        PaginaAtual = _contasViewModel;
    }

    [RelayCommand]
    private void IrParaGrupos()
    {
        _gruposViewModel.PersonagensDisponiveis();
        PaginaAtual = _gruposViewModel;
    }
    [RelayCommand]
    private void IrParaMacros()
    {
        PaginaAtual = _macrosViewModel;
    }
}
