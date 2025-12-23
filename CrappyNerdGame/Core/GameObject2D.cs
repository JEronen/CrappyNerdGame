using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public class GameObject2D
{
    protected Image Sprite { get; }

    public ObjectType Type { get; }

    public double X
    {
        get => Canvas.GetLeft(Sprite);
        set => Canvas.SetLeft(Sprite, value);
    }

    public double Y
    {
        get => Canvas.GetTop(Sprite);
        set => Canvas.SetTop(Sprite, value);
    }

    public double Width => Sprite.Width;

    public double Height => Sprite.Height;

    private Rect HitBox => new(X, Y, Width-12, Height-12);

    public GameObject2D(Image sprite, ObjectType type)
    {
        Sprite = sprite;
        Sprite.RenderTransformOrigin = new Point(0.5, 0.5);
        Type = type;
    }

    public void SetPosition(Vector2 position) => (X, Y) = (position.X, position.Y);

    public bool IntersectsWith(GameObject2D other) => HitBox.IntersectsWith(other.HitBox);


    public void SetRotation(double angle) => Sprite.RenderTransform = new RotateTransform(angle);
}