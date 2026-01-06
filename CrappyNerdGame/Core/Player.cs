using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CrappyNerdGame.Core.Components;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public sealed class Player : GameObject2D
{
    private readonly (Color Alive, Color Dead) m_colors = (Alive: Colors.White, Dead : Colors.Red);
    private readonly DropShadowEffect m_dropShadowEffect;

#if DEBUG_COLLISIONS
    private readonly DebugRectColliderRenderer m_debugColliderRenderer;
#endif

    public RectCollider2D Collider { get; }

    public Player(Image sprite, Canvas canvas) : base(sprite, ObjectType.Player)
    {
        m_dropShadowEffect = (DropShadowEffect)sprite.Effect;

        Collider = new RectCollider2D(owner: this,
            paddingX: 10,
            paddingY: 17,
            paddingWidth: -15,
            paddingHeight: -20);

#if DEBUG_COLLISIONS
        m_debugColliderRenderer = new DebugRectColliderRenderer(Transform, Collider, canvas);
#endif
    }

    public void Reset() => m_dropShadowEffect.Color = m_colors.Alive;

    public void TakeDamage() => m_dropShadowEffect.Color = m_colors.Dead;

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