using System.Windows.Controls;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public sealed class PipePair
{
    private GameObject2D Top { get; }
    private GameObject2D Bottom { get; }

    public double X
    {
        get => Top.X;
        set
        {
            Top.X = value;
            Bottom.X = value;
        }
    }

    public PipePair(Image top, Image bottom)
    {
        Top = new GameObject2D(top, ObjectType.PipeTop);
        Bottom = new GameObject2D(bottom, ObjectType.PipeBottom);
    }

    public void SetGap(double centerY, double gap)
    {
        var half = gap * 0.5f;
        Top.Y = centerY + half;
        Bottom.Y = centerY - half - Bottom.Height;
    }

    public bool IntersectsWith(GameObject2D other) => Top.IntersectsWith(other) || Bottom.IntersectsWith(other);
}