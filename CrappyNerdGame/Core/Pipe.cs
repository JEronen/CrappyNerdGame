using System.Windows.Controls;
using CrappyNerdGame.Core.Components;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public sealed class Pipe : GameObject2D
{
#if DEBUG_COLLISIONS
    private readonly DebugRectColliderRenderer m_debugColliderRenderer;
#endif
    public RectCollider2D Collider { get; }

    public Pipe(Image sprite, ObjectType type, Canvas canvas) : base(sprite, type)
    {
        Collider = new RectCollider2D(this, paddingWidth:-1, paddingHeight:-1);
#if DEBUG_COLLISIONS
        m_debugColliderRenderer = new DebugRectColliderRenderer(Transform, Collider, canvas);
#endif
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
#if DEBUG_COLLISIONS
        if (disposing)
        {
            m_debugColliderRenderer.Dispose();
        }
#endif
    }
}