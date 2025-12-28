using System.Windows.Controls;
using CrappyNerdGame.Args;
using CrappyNerdGame.Core;
using CrappyNerdGame.Enums;
using CrappyNerdGame.ViewModels;

namespace CrappyNerdGame.Views;

public abstract class GameViewBase: UserControl
{
    public event EventHandler<NavigationEventArgs>? ViewRequested;

    public ViewType ViewType { get; protected init; }

    protected void RequestView(ViewType type) => ViewRequested?.Invoke(this, new NavigationEventArgs(type));

    protected void RequestView(ViewType type, GameStatsUpdate statsUpdate) => ViewRequested?.Invoke(this, new NavigationEventArgs(type, statsUpdate));

    public virtual void Activate(MainWindowViewModel mainViewModel)
    {
    }
}