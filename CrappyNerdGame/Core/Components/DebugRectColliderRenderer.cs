using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using CrappyNerdGame.Core;
using CrappyNerdGame.Core.Components;

// ReSharper disable once UnusedType.Global Used during Debug-mode when DEBUG_COLLISIONS is defined.
public sealed class DebugRectColliderRenderer : IDisposable
{
    private readonly Transform2D m_transform;
    private readonly RectangleGeometry m_hitRectGeometry = new();
    private readonly RectCollider2D m_collider;

    public DebugRectColliderRenderer(Transform2D transform, RectCollider2D collider, Canvas canvas)
    {
        m_transform = transform;
        m_collider = collider;
        var path = new Path
        {
            Stroke = Brushes.Red,
            StrokeThickness = 1.5,
            Fill = Brushes.Transparent,
            Data = m_hitRectGeometry,
            IsHitTestVisible = false
        };
        canvas.Children.Add(path);
        m_transform.Changed += OnTransformChanged;
    }

    private void OnTransformChanged(TransformEventType obj) => m_hitRectGeometry.Rect = m_collider.HitBox;

    public void Dispose() => m_transform.Changed -= OnTransformChanged;
}