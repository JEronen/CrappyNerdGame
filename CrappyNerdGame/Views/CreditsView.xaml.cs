using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CrappyNerdGame.Enums;

namespace CrappyNerdGame.Views;

public partial class CreditsView : GameViewBase
{
    private const double Speed = 26;

    private bool m_isScrolling;
    private double m_y;

    public CreditsView()
    {
        ViewType = ViewType.Credits;
        InitializeComponent();
    }

    public override void Activate()
    {
        m_y = 500;
        CreditsTransform.Y = m_y;
        m_isScrolling = true;
        CompositionTarget.Rendering += OnRendering;
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        if (!m_isScrolling)
            return;

        m_y -= Speed / 60.0;
        CreditsTransform.Y = m_y;

        if (m_y < -CreditsPanel.ActualHeight)
            StopScrolling();
    }

    private void StopScrolling()
    {
        m_isScrolling = false;
        CompositionTarget.Rendering -= OnRendering;
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e) => Exit();

    protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e) => Exit();

    private void Exit()
    {
        StopScrolling();
        RequestView(ViewType.StartMenu);
    }

    private void CreditsView_OnLoaded(object sender, RoutedEventArgs e)
    {
        Keyboard.Focus(this);
        Focus();
    }

    private void CreditsView_OnUnloaded(object sender, RoutedEventArgs e) => StopScrolling();
}