using FastEnumUtility;

namespace CrappyNerdGame.Enums;

public enum MusicType
{
    [Label("TempestLoop1.wav")]
    Gameplay,
    [Label("EchoesAtDuskLoop1.wav")]
    StartMenu
}

public static class MusicTypeExtensions
{
    extension(MusicType type)
    {
        public string FileName => type.GetLabel(0) ?? string.Empty;
    }
}