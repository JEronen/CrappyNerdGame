using System.Diagnostics;
using System.IO;
using CrappyNerdGame.Enums;
using FastEnumUtility;
using NAudio.CoreAudioApi;
using NAudio.Extras;
using NAudio.Wave;

namespace CrappyNerdGame.Core;

public sealed class AudioPlayer : IDisposable
{
    private readonly AudioPlaybackEngine m_engine;
    private readonly Dictionary<SfxType, CachedSound> m_soundEffects;
    private WasapiOut? m_output;
    private AudioFileReader? m_reader;
    private bool m_isPlaying;

    public AudioPlayer(string sfxPath = "Assets/SFX/")
    {
        m_engine = new AudioPlaybackEngine();
        m_soundEffects = FastEnum.GetValues<SfxType>()
            .ToDictionary(x => x, x => new CachedSound(Path.Combine(sfxPath, x.GetFileName())));
    }

    public void PlaySfx(SfxType type) => m_engine.PlaySound(m_soundEffects[type]);

    public void Play(string filePath, bool loop = true)
    {
        Debug.Assert(!m_isPlaying, "Audio should be stopped before playing again!");
        m_reader = new AudioFileReader(filePath);
        m_output = new WasapiOut(AudioClientShareMode.Shared, 10);
        if(loop)
        {
            var loopReader = new LoopStream(m_reader);
            m_output.Init(loopReader);
        }
        else
        {
            m_output.Init(m_reader);
        }
        m_output.Play();
        m_isPlaying = true;
    }

    public void Stop()
    {
        m_output?.Stop();
        m_output?.Dispose();
        m_reader?.Dispose();
        m_isPlaying = false;
    }

    public void Dispose()
    {
        m_output?.Dispose();
        m_reader?.Dispose();
    }
}