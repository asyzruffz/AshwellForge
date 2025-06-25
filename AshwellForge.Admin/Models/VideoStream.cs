namespace AshwellForge.Admin.Models;

public class VideoStream
{
    public string Id { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public string StreamPath { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
    public int SubscribersCount { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public int Framerate { get; set; }
    public int VideoCodecId { get; set; }
    public int AudioCodecId { get; set; }
    public int AudioSampleRate { get; set; }
    public int AudioChannels { get; set; }
    public IDictionary<string, string> StreamArguments { get; set; } = new Dictionary<string, string>();
}
