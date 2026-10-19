using Avalonia.Controls;
using Avalonia.Win32.Input;
using NovoProjetoAvalonia.ViewModels;
using System;
using System.Runtime.InteropServices;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;

namespace NovoProjetoAvalonia.Views;

public partial class MainWindow : Window
{

    private IntPtr _hwnd;
    private const int HOTKEY_ID = 9001;

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

    [DllImport("user32.dll")]
    private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
    private WndProcDelegate? _newWndProc;
    private IntPtr _oldWndProc = IntPtr.Zero;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnWindowOpened(object? sender, EventArgs e)
    {
        var handle = this.TryGetPlatformHandle();
        if (handle == null) return;

        _hwnd = handle.Handle;
        uint vkF1 = (uint)KeyInterop.VirtualKeyFromKey(Key.F1);

        // Registra o F1 globalmente
        RegisterHotKey(_hwnd, HOTKEY_ID, 0x0000, vkF1);

        // Intercepta a fila de mensagens do Win32 de forma segura para o HWND do Avalonia
        _newWndProc = CustomWndProc;
        IntPtr functionPointer = Marshal.GetFunctionPointerForDelegate(_newWndProc);

        // GWL_WNDPROC = -4
        _oldWndProc = SetWindowLongPtr(_hwnd, -4, functionPointer);
    }

    private IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
    {
        const uint WM_HOTKEY = 0x0312;

        if (msg == WM_HOTKEY && (int)wParam == HOTKEY_ID)
        {
            Dispatcher.UIThread.Post(() =>
            {
                if (DataContext is GrupoViewModel vm && vm.SeguirLiderCommand.CanExecute(null))
                {
                    vm.SeguirLiderCommand.Execute(null);
                }
            });
        }

        return CallWindowProc(_oldWndProc, hWnd, msg, wParam, lParam);
    }

    private void OnWindowClosing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_hwnd != IntPtr.Zero)
        {
            UnregisterHotKey(_hwnd, HOTKEY_ID);

            // Restaura o procedimento de janela original do Windows ao fechar
            if (_oldWndProc != IntPtr.Zero)
            {
                SetWindowLongPtr(_hwnd, -4, _oldWndProc);
            }
        }
    }
}