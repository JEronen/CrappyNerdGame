using System.ComponentModel;
using System.Windows.Media.Animation;
using CrappyNerdGame.Enums;
using CrappyNerdGame.ViewModels;

namespace CrappyNerdGame;

public partial class MainWindow
{
    private readonly MainWindowViewModel m_viewModel;

    public MainWindow()
    {
        InitializeComponent();
        m_viewModel = (MainWindowViewModel)DataContext;
        m_viewModel.FadeRequested += OnFadeRequested;
    }

    private void OnFadeRequested(object? sender, FadeType e)
    {
        var storyboard = (Storyboard)FindResource(e);
        storyboard.Begin();
    }

    private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        m_viewModel.FadeRequested -= OnFadeRequested;
        m_viewModel.Dispose();
    }
}