using System.Diagnostics;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CrappyNerdGame.Core;
using CrappyNerdGame.Enums;
using CrappyNerdGame.Extensions;
using CrappyNerdGame.ViewModels;

namespace CrappyNerdGame.Views;

public sealed partial class GamePlayView : GameViewBase, IDisposable
{
    private const int FlapRotation = -20;
    private const int FallRotation = 5;
    private const int Gravity = 1800;
    private const int FlapImpulse = -400;
    private const int MaxFallSpeed = 950;
    private const int PipeSpeed = 200;
    private const int PipeCount = 3;
    private const int PipeX = 500;
    private const int PipeSpacing = 300;
    private const int PipeLeftKillX = -100;

    private double m_velocityY;
    private readonly Player m_player;
    private bool m_isFlapping;
    private readonly Vector2 m_playerSpawnPosition = new(x: 20, y: 190);

    private readonly List<PipePair> m_pipePairs = [];
    private readonly List<GameObject2D> m_skyElements = [];

    private readonly Vector2[] m_skyElementSpawnPositions =
    [
        new(x: 600, y: 119),
        new(x: 1100, y: 122),
        new(x: 1400, y: 100),
    ];

    private const double SkyElementLeftKillX = -260;
    private const int SkyElementSpeed = 40;

    private DateTime m_lastFrameTime;

    private AudioPlayer m_audioPlayer;
    private bool m_isRunning;

    public double Score
    {
        get;
        set
        {
            if (Math.Abs(field - value) > 0.1)
            {
                field = value;
                ScoreLabel.Content = "Score : " + value;
            }
        }
    }

    public GamePlayView()
    {
        InitializeComponent();
        ViewType = ViewType.GamePlay;
        m_player = new Player(PlayerSprite);
        CreateSkyElements();
        CreatePipes();
    }

    private void CreateSkyElements()
    {
        foreach (var image in GameCanvas.Children.OfType<Image>()
                     .Where(x => x.Tag is ObjectType.SkyElement))
            m_skyElements.Add(new GameObject2D(image, ObjectType.SkyElement));
    }

    private void CreatePipes()
    {
        for (int i = 0; i < PipeCount; i++)
        {
            var top = PipeUpTemplate.Clone();
            var bottom = PipeDownTemplate.Clone();

            GameCanvas.Children.Add(top);
            GameCanvas.Children.Add(bottom);

            var pair = new PipePair(top, bottom)
            {
                X = PipeX + i * PipeSpacing
            };
            m_pipePairs.Add(pair);
        }
    }

    private void ResetPipes()
    {
        for (int i = 0; i < m_pipePairs.Count; i++)
        {
            var pipe = m_pipePairs[i];
            pipe.X = PipeX + i * PipeSpacing;
            RandomizePipe(pipe);
        }
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        if(!m_isRunning)
            return;
        var deltaTime = CalculateDeltaTime();

        m_velocityY += Gravity * deltaTime;
        m_velocityY = Math.Min(m_velocityY, MaxFallSpeed);
        m_player.Y += m_velocityY * deltaTime;

        if (IsOutsideOfScreen(m_player))
        {
            EndGame();
            return;
        }

        foreach (var pipePair in m_pipePairs)
        {
            pipePair.X -= PipeSpeed * deltaTime;
            if (pipePair.X < PipeLeftKillX)
            {
                MoveToEnd(pipePair);
                Score++;
            }

            if (pipePair.IntersectsWith(m_player))
            {
                m_audioPlayer.Stop();
                m_audioPlayer.PlaySfx(SfxType.Boom);
                m_player.TakeDamage();
                EndGame();
                return;
            }
        }

        var skyElementXIncrease = Width + 200;

        foreach (var skyElement in m_skyElements)
        {
            skyElement.X -= SkyElementSpeed * deltaTime;
            if (skyElement.X < SkyElementLeftKillX)
            {
                skyElement.X += skyElementXIncrease;
            }
        }
    }

    private double CalculateDeltaTime()
    {
        var now = DateTime.Now;
        var deltaTime = (now - m_lastFrameTime).TotalSeconds;
        m_lastFrameTime = now;
        return deltaTime;
    }

    private void MoveToEnd(PipePair pipe)
    {
        double maxX = m_pipePairs.Max(p => p.X);
        pipe.X = maxX + PipeSpacing;
        RandomizePipe(pipe);
    }

    private static void RandomizePipe(PipePair pipe)
    {
        double centerY = Random.Shared.Next(200, 450);
        pipe.SetGap(centerY, gap: 140);
    }

    private bool IsOutsideOfScreen(Player player) => player.Y < -player.Height || (player.Y  > Height);

    public override void Activate(MainWindowViewModel mainViewModel)
    {
        m_audioPlayer = mainViewModel.AudioPlayer;
        m_audioPlayer.Play("Assets/Music/TempestLoop1.wav");
        StartGame();
    }

    private void StartGame()
    {
        GameCanvas.Focus();
        Keyboard.Focus(GameCanvas);
        Score = 0;

        m_player.SetPosition(m_playerSpawnPosition);
        m_player.Activate();

        ResetPipes();

        Debug.Assert(m_skyElements.Count == m_skyElementSpawnPositions.Length);

        for (var i = 0; i < m_skyElements.Count && i < m_skyElementSpawnPositions.Length; i++)
        {
            m_skyElements[i].SetPosition(m_skyElementSpawnPositions[i]);
        }

        CompositionTarget.Rendering += OnRendering;
        m_lastFrameTime = DateTime.Now;
        m_isRunning = true;
    }

    private void EndGame()
    {
        m_isRunning = false;
        CompositionTarget.Rendering -= OnRendering;
        RequestView(ViewType.GameOver, new GameStatsUpdate(Score));
    }

    private void GameCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (m_isFlapping)
            return;

        m_isFlapping = true;
        m_audioPlayer.PlaySfx(SfxType.Jump);
        m_player.SetRotation(FlapRotation);
        m_velocityY = FlapImpulse;
    }

    private void GameCanvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        m_player.SetRotation(FallRotation);
        m_isFlapping = false;
    }

    public void Dispose() => CompositionTarget.Rendering -= OnRendering;
}