using System.Windows;
using CrappyNerdGame.Enums;
using CrappyNerdGame.ViewModels;

namespace CrappyNerdGame.Views;

public partial class StartMenuView : GameViewBase
{
    public StartMenuView()
    {
        InitializeComponent();
        ViewType = ViewType.StartMenu;
    }

    public override void Activate(MainWindowViewModel mainViewModel) => mainViewModel.AudioPlayer.Play("Assets/Music/EchoesAtDuskLoop1.wav");

    private void RequestViewButtonClick(object sender, RoutedEventArgs e)
    {
        var viewType = (ViewType)((FrameworkElement)sender).Tag;
        RequestView(viewType);
    }

    private void QuitButtonClick(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
}