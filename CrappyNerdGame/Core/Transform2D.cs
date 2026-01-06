using System.Numerics;

namespace CrappyNerdGame.Core;

public enum TransformEventType
{
    Position,
    Rotation
}

public sealed class Transform2D
{
    public Vector2 Position { get; private set; }
    public double Rotation { get; private set; }

    public event Action<TransformEventType>? Changed;

    public void Translate(Vector2 delta)
    {
        if (delta == Vector2.Zero) return;
        SetPosition(Position + delta);
    }

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