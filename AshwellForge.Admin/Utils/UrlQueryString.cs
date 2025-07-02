using System.Collections.Specialized;
using System.Web;

namespace AshwellForge.Admin.Utils;

public class UrlQueryString
{
    NameValueCollection data;

    public static UrlQueryString Create() => new UrlQueryString(string.Empty);
    public static UrlQueryString From(string existingData) => new UrlQueryString(existingData);

    private UrlQueryString(string existingData)
    {
        data = HttpUtility.ParseQueryString(existingData);
    }

    public UrlQueryString Set(string key, string value)
    {
        data[key] = value;
        return this;
    }

    public string? Get(string key) => data[key];

    public string Build() => $"?{data.ToString() ?? string.Empty}";
}
