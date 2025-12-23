namespace CrappyNerdGame.Core;

public struct GameStatsUpdate
{
    public static readonly GameStatsUpdate Empty = new();

    public readonly double Score;

    public GameStatsUpdate()
    {
    }

    public  GameStatsUpdate(double score) => Score = score;
}