using System.Media;

namespace CrappyNerdGame.Core;

public static class AudioPlayer
{
    private static SoundPlayer? soundPlayer;

    public static void Play(string soundLocation, bool loop = true)
    {
        soundPlayer = new SoundPlayer(soundLocation);
        soundPlayer.Load();
        if(loop)
        {
            soundPlayer.PlayLooping();
        }
        else
        {
            soundPlayer.Play();
        }
    }

    public static void Stop()
    {
        if(soundPlayer == null)
            return;
        soundPlayer.Stop();
        soundPlayer.Dispose();
        soundPlayer = null;
    }
}