using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CrappyNerdGame.Enums;
using CrappyNerdGame.ViewModels;

namespace CrappyNerdGame.Views;

public sealed partial class CreditsView : GameViewBase
{
    private const double Speed = 95;

    private bool m_isScrolling;
    private double m_y;
    private Stopwatch m_stopwatch;
    private DateTime m_lastFrameTime;

    public CreditsView()
    {
        ViewType = ViewType.Credits;
        InitializeComponent();
    }

    public override void Activate(MainWindowViewModel mainViewModel)
    {
        m_y = 500;
        m_stopwatch = Stopwatch.StartNew();
        m_lastFrameTime = DateTime.Now;
        CreditsTransform.Y = m_y;
        m_isScrolling = true;
        CompositionTarget.Rendering += OnRendering;
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        if (!m_isScrolling)
            return;

        var now = DateTime.Now;
        var deltaTime = (now - m_lastFrameTime).TotalSeconds;
        m_lastFrameTime = now;

        m_y -= Speed * deltaTime;
        CreditsTransform.Y = m_y;

        if (m_y < -CreditsPanel.ActualHeight)
            StopScrolling();
    }

    private void StopScrolling()
    {
        m_isScrolling = false;
        m_stopwatch.Reset();
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