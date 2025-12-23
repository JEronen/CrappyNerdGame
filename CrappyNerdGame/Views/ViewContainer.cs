using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Views;

public class ViewContainer
{
    private static readonly Dictionary<ViewType, GameViewBase> Views = new();

    static ViewContainer()
    {
       Views.Add(ViewType.StartMenu, new StartMenuView());
       Views.Add(ViewType.GamePlay, new GamePlayView());
       Views.Add(ViewType.GameOver, new GameOverView());
       Views.Add(ViewType.Credits, new CreditsView());
    }

    public GameViewBase GetView(ViewType viewType) => Views.TryGetValue(viewType, out var view)
        ? view
        : throw new KeyNotFoundException("View not found at " + nameof(ViewContainer));

    public void CleanUp()
    {
        foreach (var view in Views.Values.OfType<IDisposable>())
        {
            view.Dispose();
        }
    }
}