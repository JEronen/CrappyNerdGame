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
    public static string GetFileName(this SfxType type) => type.GetLabel(0) ?? string.Empty;
}