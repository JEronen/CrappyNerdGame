using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrappyNerdGame.Core.Components;

public sealed class SpriteRenderer2D : IDisposable
{
    private readonly Image m_sprite;
    private readonly Transform2D m_transform;

    public double Width => m_sprite.Width;
    public double Height => m_sprite.Height;

    public Size Size => new(Width, Height);

    public SpriteRenderer2D(Image sprite, Transform2D transform)
    {
        m_sprite = sprite;
        m_sprite.RenderTransformOrigin = new Point(0.5f, 0.5f);
        m_transform = transform;
        m_transform.Changed += OnTransformChanged;
    }

    private void OnTransformChanged(TransformEventType eventType)
    {
        switch (eventType)
        {
            case TransformEventType.Position:
                Canvas.SetLeft(m_sprite, m_transform.Position.X);
                Canvas.SetTop(m_sprite, m_transform.Position.Y);
                break;
            case TransformEventType.Rotation:
                m_sprite.RenderTransform = new RotateTransform(m_transform.Rotation);
                break;
        }
    }

    public void Dispose() => m_transform.Changed -= OnTransformChanged;
}