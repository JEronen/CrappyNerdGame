using FastEnumUtility;

namespace CrappyNerdGame.Enums;

public enum SfxType
{
    [Label("Jump.wav")]
    Jump,
    [Label("Boom.wav")]
    Boom
}

public static class SfxTypeExtensions
{
    extension(SfxType type)
    {
        public string FileName => type.GetLabel(0) ?? string.Empty;
    }
}