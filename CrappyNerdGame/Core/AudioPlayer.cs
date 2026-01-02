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
    private readonly Dictionary<MusicType, string> m_musicFilePaths;

    private WasapiOut? m_output;
    private AudioFileReader? m_reader;
    private bool m_isPlaying;

    public AudioPlayer(string sfxPath, string musicPath)
    {
        m_engine = new AudioPlaybackEngine();
        m_soundEffects = FastEnum.GetValues<SfxType>()
            .ToDictionary(x => x, x => new CachedSound(Path.Combine(sfxPath, x.FileName)));
        m_musicFilePaths = FastEnum.GetValues<MusicType>()
            .ToDictionary(x => x, x => Path.Combine(musicPath, x.FileName));
    }

    public void PlaySfx(SfxType type) => m_engine.PlaySound(m_soundEffects[type]);

    public void Play(MusicType type, bool loop = true)
    {
        Debug.Assert(!m_isPlaying, "Audio should be stopped before playing again!");
        var filePath = m_musicFilePaths[type];
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
        m_output?.Dispose();
        m_reader?.Dispose();
        m_isPlaying = false;
    }

    public void Dispose() => Stop();
}