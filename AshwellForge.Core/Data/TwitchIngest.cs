using System.Text;
using System.Text.Json.Serialization;

namespace AshwellForge.Core.Data;

public class TwitchIngest
{
    [JsonPropertyName("_id")]
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url_template")]
    public string UrlTemplate { get; set; } = string.Empty;

    [JsonPropertyName("url_template_secure")]
    public string UrlTemplateSecure { get; set; } = string.Empty;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(Id).Append(" - ").AppendLine(Name);
        sb.AppendLine(UrlTemplate).Append(UrlTemplateSecure);
        return sb.ToString();
    }
}

public record TwitchIngests(IEnumerable<TwitchIngest> Ingests);
