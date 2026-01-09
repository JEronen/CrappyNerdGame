using System.Windows.Controls;
using CrappyNerdGame.Core.Components;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Core;

public class GameObject2D : IDisposable
{
    private bool m_isDisposed;

    public Transform2D Transform { get; } = new();
    public SpriteRenderer2D SpriteRenderer { get; }

    public ObjectType Type { get; }

    public double Width => SpriteRenderer.Width;

    public double Height => SpriteRenderer.Height;

    public GameObject2D(Image sprite, ObjectType type)
    {
        SpriteRenderer = new SpriteRenderer2D(sprite, Transform);
        Type = type;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!m_isDisposed && disposing)
        {
            SpriteRenderer.Dispose();
            m_isDisposed = true;
        }
    }
}