using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrappyNerdGame.Args;
using CrappyNerdGame.Core;
using CrappyNerdGame.Enums;
using CrappyNerdGame.Views;

namespace CrappyNerdGame.ViewModels;

public sealed partial class MainWindowViewModel : ObservableObject, IDisposable
{
    public event EventHandler<FadeType>? FadeRequested;

    private readonly ViewContainer m_viewContainer;

    [ObservableProperty]
    private GameViewBase? m_activeView;

    [ObservableProperty]
    private bool m_isGameOver;

    [ObservableProperty]
    private double m_score;

    public RelayCommand RestartCommand { get; }

    public RelayCommand StartMenuCommand { get; }

    public AudioPlayer AudioPlayer { get; }

    public MainWindowViewModel()
    {
        AudioPlayer = new AudioPlayer(sfxPath: "Assets/SFX/", musicPath: "Assets/Music/");
        m_viewContainer = new ViewContainer();
        SetView(ViewType.StartMenu);
        RestartCommand = new RelayCommand(() => SetView(ViewType.GamePlay));
        StartMenuCommand = new RelayCommand (() => SetView(ViewType.StartMenu));
    }

    private void SetView(ViewType type)
    {
        IsGameOver = false;
        AudioPlayer.Stop();
        FadeRequested?.Invoke(this, FadeType.FadeFromBlack);
        if (ActiveView != null)
        {
            if (ActiveView.ViewType == type)
            {
                ActiveView.Activate(this);
                return;
            }
            ActiveView.ViewRequested -= OnViewRequested;
        }
        ActiveView = m_viewContainer.GetView(type);
        ActiveView.Activate(this);
        ActiveView.ViewRequested += OnViewRequested;
    }

    private void OnViewRequested(object? sender, NavigationEventArgs e)
    {
        if (e.ViewType == ViewType.GameOver)
        {
            IsGameOver = true;
            Score = e.StatsUpdate.Score;
            return;
        }
        SetView(e.ViewType);
    }

    public void Dispose()
    {
        AudioPlayer.Dispose();
        if (ActiveView != null)
        {
            ActiveView.ViewRequested -= OnViewRequested;
        }
        m_viewContainer.CleanUp();
    }
}