namespace AshwellForge.Admin.Models;

/// <summary>
/// Video codecs.
/// </summary>
public enum VideoCodec : byte
{
    SorensonH263 = 2,
    ScreenVideo = 3,
    On2VP6 = 4,
    On2VP6WithAlphaChannel = 5,
    ScreenVideoVersion2 = 6,
    AVC = 7,
    HEVC = 12,
    AV1 = 13
}

/// <summary>
/// Audio codecs.
/// </summary>
public enum AudioCodec
{
    LinearPCMPlatformEndian = 0,
    ADPCM = 1,
    MP3 = 2,
    LinearPCMLittleEndian = 3,
    Nellymoser16kHzMono = 4,
    Nellymoser8kHzMono = 5,
    Nellymoser = 6,
    G711ALawLogarithmicPCM = 7,
    G711MuLawLogarithmicPCM = 8,
    Reserved = 9,
    AAC = 10,
    Speex = 11,
    Opus = 13,
    MP38kHz = 14,
    DeviceSpecificSound = 15
}

public static class CodecUtils
{
    public static string ToName(this VideoCodec videoCodec) => videoCodec switch
    {
        VideoCodec.SorensonH263 => "H.263",
        VideoCodec.ScreenVideo => "Screen Video",
        VideoCodec.On2VP6 => "VP6",
        VideoCodec.On2VP6WithAlphaChannel => "VP6a",
        VideoCodec.ScreenVideoVersion2 => "Screen Video V2",
        VideoCodec.AVC => "AVC",
        VideoCodec.HEVC => "HEVC",
        VideoCodec.AV1 => "AV1",
        _ => "Unknown",
    };

    public static string ToName(this AudioCodec audioCodec) => audioCodec switch
    {
        AudioCodec.LinearPCMPlatformEndian => "LPCM",
        AudioCodec.ADPCM => "ADPCM",
        AudioCodec.MP3 => "MP3",
        AudioCodec.LinearPCMLittleEndian => "LPCM",
        AudioCodec.Nellymoser16kHzMono => "Asao 16kHz",
        AudioCodec.Nellymoser8kHzMono => "Asao 8kHz",
        AudioCodec.Nellymoser => "Asao",
        AudioCodec.G711ALawLogarithmicPCM => "G.711 PCM A-law",
        AudioCodec.G711MuLawLogarithmicPCM => "G.711 PCM μ-law",
        AudioCodec.Reserved => "Reserved",
        AudioCodec.AAC => "AAC",
        AudioCodec.Speex => "Speex",
        AudioCodec.Opus => "Opus",
        AudioCodec.MP38kHz => "MP3 8kHz",
        AudioCodec.DeviceSpecificSound => "Device",
        _ => "Unknown",
    };
}
