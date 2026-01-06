using System.Windows;

namespace CrappyNerdGame.Core.Components;

public sealed class RectCollider2D(GameObject2D owner, double paddingX = 0, double paddingY = 0, double paddingWidth = 0, double paddingHeight = 0)
{
    private double PaddingX { get; } = paddingX;
    private double PaddingWidth { get; } = paddingWidth;
    private double PaddingY { get; } = paddingY;
    private double PaddingHeight { get; } = paddingHeight;

    public Rect HitBox
    {
        get
        {
            var size = owner.SpriteRenderer.Size;
            return new Rect(
                owner.Transform.Position.X + PaddingX,
                owner.Transform.Position.Y + PaddingY,
                size.Width + PaddingWidth,
                size.Height + PaddingHeight
            );
        }
    }

    public bool Intersects(RectCollider2D other) => HitBox.IntersectsWith(other.HitBox);
}