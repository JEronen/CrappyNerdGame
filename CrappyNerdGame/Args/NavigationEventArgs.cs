using CrappyNerdGame.Core;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Args;

public sealed class NavigationEventArgs(ViewType viewType, GameStatsUpdate statsUpdate) : EventArgs
{
    public ViewType ViewType { get; } = viewType;

    public GameStatsUpdate StatsUpdate { get; init; } = statsUpdate;

    public NavigationEventArgs(ViewType viewType) : this(viewType, GameStatsUpdate.Empty){}
}