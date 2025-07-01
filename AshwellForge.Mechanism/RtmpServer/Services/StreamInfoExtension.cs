using LiveStreamingServerNet.Rtmp.Server.Contracts;
using Stream = AshwellForge.Mechanism.RtmpServer.Dtos.Stream;

namespace AshwellForge.Mechanism.RtmpServer.Services;

public static class StreamInfoExtension
{
    public static Stream ToDto(this IRtmpStreamInfo stream)
    {
        int videoCodecId = 0;
        int height = 0;
        int width = 0;
        int framerate = 0;
        int audioCodecId = 0;
        int audioSampleRate = 0;
        int audioChannels = 0;

        if (stream.MetaData != null)
        {
            videoCodecId = stream.MetaData.GetIntValue("videocodecid", 0);
            height = stream.MetaData.GetIntValue("height", 0);
            width = stream.MetaData.GetIntValue("width", 0);
            framerate = stream.MetaData.GetIntValue("framerate", 0);

            audioCodecId = stream.MetaData.GetIntValue("audiocodecid", 0);
            audioSampleRate = stream.MetaData.GetIntValue("audiosamplerate", 0);
            audioChannels = stream.MetaData.GetIntValue("audiochannels", 0);

            if (audioChannels == 0 && stream.MetaData.TryGetValue("stereo", out var _stereoValue) && _stereoValue is bool stereoValue)
                audioChannels = stereoValue ? 2 : 1;
        }

        return new Stream
        {
            Id = $"{stream.Publisher.Id}@{stream.StreamPath}",
            ClientId = stream.Publisher.Id,
            StreamPath = stream.StreamPath,
            SubscribersCount = stream.Subscribers.Count,
            StartTime = stream.StartTime,
            StreamArguments = stream.StreamArguments,

            VideoCodecId = videoCodecId,
            Height = height,
            Width = width,
            Framerate = framerate,

            AudioCodecId = audioCodecId,
            AudioSampleRate = audioSampleRate,
            AudioChannels = audioChannels,
        };
    }

    private static int GetIntValue(this IReadOnlyDictionary<string, object> dictionary, string key, int defaultValue)
    {
        if (dictionary.TryGetValue(key, out var _doublValue) && _doublValue is double doubleValue)
            return (int)doubleValue;

        if (dictionary.TryGetValue(key, out var _intValue) && _intValue is int intValue)
            return intValue;

        return defaultValue;
    }
}
