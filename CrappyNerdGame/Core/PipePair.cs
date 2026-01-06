using System.Windows.Controls;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public sealed class PipePair : IDisposable
{
    private Pipe Top { get; }
    private Pipe Bottom { get; }

    public double X
    {
        get => Top.Transform.Position.X;
        set
        {
            Top.Transform.SetX(value);
            Bottom.Transform.SetX(value);
        }
    }

    public PipePair(Image top, Image bottom, Canvas canvas)
    {
        Top = new Pipe(top, ObjectType.PipeTop, canvas);
        Bottom = new Pipe(bottom, ObjectType.PipeBottom, canvas);
        canvas.Children.Add(top);
        canvas.Children.Add(bottom);
    }

    public void SetGap(double centerY, double gap)
    {
        var half = gap * 0.5f;
        Top.Transform.SetY(centerY + half);
        Bottom.Transform.SetY(centerY - half - Bottom.Height);
    }

    public bool IntersectsWith(Player player)
    {
        var otherCollider = player.Collider;

        return Top.Collider.Intersects(otherCollider) ||
               Bottom.Collider.Intersects(otherCollider);
    }

    public void Dispose()
    {
        Top.Dispose();
        Bottom.Dispose();
    }
}