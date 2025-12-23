using System.Windows;
using System.Windows.Input;

namespace CrappyNerdGame.Views;

public partial class GameOverView : GameViewBase
{
    public static readonly DependencyProperty ScoreProperty =
        DependencyProperty.Register(
            nameof(Score),
            typeof(double),
            typeof(GameOverView),
            new FrameworkPropertyMetadata(double.NaN, OnScoreChanged));

    private static void OnScoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var view = (GameOverView) d;
        view.ScoreTextBlock.Text = "Final Score: " + (e.NewValue as double?);
    }

    public double Score
    {
        get => (double) GetValue(ScoreProperty);
        set => SetValue(ScoreProperty, value);
    }

    public static readonly DependencyProperty RestartCommandProperty =
        DependencyProperty.Register(
            nameof(RestartCommand),
            typeof(ICommand),
            typeof(GameOverView));

    public ICommand RestartCommand
    {
        get => (ICommand) GetValue(RestartCommandProperty);
        set => SetValue(RestartCommandProperty, value);
    }

    public static readonly DependencyProperty StartMenuCommandProperty =
        DependencyProperty.Register(
            nameof(StartMenuCommand),
            typeof(ICommand),
            typeof(GameOverView));

    public ICommand StartMenuCommand
    {
        get => (ICommand) GetValue(StartMenuCommandProperty);
        set => SetValue(StartMenuCommandProperty, value);
    }

    public GameOverView() => InitializeComponent();

    private void RestartButton_OnClick(object sender, RoutedEventArgs e) => RestartCommand?.Execute(null);

    private void MainMenuButton_OnClick(object sender, RoutedEventArgs e) => StartMenuCommand?.Execute(null);
}