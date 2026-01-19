using System.Numerics;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public sealed class Transform2D
{
    public Vector2 Position { get; private set; }
    public double Rotation { get; private set; }

    public event Action<TransformEventType>? Changed;

    public void TranslateX(double dx)
    {
        if (dx == 0) return;
        SetPosition(Position with { X = (float)(Position.X + dx) });
    }

    public void TranslateY(double dy)
    {
        if (dy == 0) return;
        SetPosition(Position with { Y = (float)(Position.Y + dy) });
    }

    public void SetPosition(Vector2 position)
    {
        Position = position;
        Changed?.Invoke(TransformEventType.Position);
    }

    public void SetX(double x) => SetPosition(Position with { X = (float)x });

    public void SetY(double y) => SetPosition(Position with { Y = (float)y });

    public void SetRotation(double rotation)
    {
        Rotation = rotation;
        Changed?.Invoke(TransformEventType.Rotation);
    }
}