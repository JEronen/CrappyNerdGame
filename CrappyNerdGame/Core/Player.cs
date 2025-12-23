using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public sealed class Player : GameObject2D
{
    private readonly (Color Alive, Color Dead) m_colors = (Alive: Colors.White, Dead : Colors.Red);

    private readonly DropShadowEffect m_dropShadowEffect;

    public Player(Image sprite) : base(sprite, ObjectType.Player) => m_dropShadowEffect = (DropShadowEffect)Sprite.Effect;

    public void Activate() => SetDropShadowEffectColor(m_colors.Alive);

    public void TakeDamage() => SetDropShadowEffectColor(m_colors.Dead);

    private void SetDropShadowEffectColor(Color color) => m_dropShadowEffect.Color = color;
}