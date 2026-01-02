using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Views;

public sealed class ViewContainer : IDisposable
{
    public required Dictionary<ViewType, GameViewBase> Views { get; init; }

    public GameViewBase GetView(ViewType viewType) => Views.TryGetValue(viewType, out var view)
        ? view
        : throw new KeyNotFoundException("View not found at " + nameof(ViewContainer));

    public void Dispose()
    {
        foreach (var view in Views.Values.OfType<IDisposable>())
        {
            view.Dispose();
        }
    }
}