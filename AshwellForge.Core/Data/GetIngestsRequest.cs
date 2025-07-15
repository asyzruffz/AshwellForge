namespace AshwellForge.Core.Data;

public record GetIngestsRequest(string kind, bool refresh = false)
{
    public GetIngestsParam ToParam()
    {
        IngestServerKind enumKind;
        if (!Enum.TryParse(kind, out enumKind))
            enumKind = IngestServerKind.Unspecified;
        return new GetIngestsParam(enumKind, refresh);
    }
}

public record GetIngestsParam(IngestServerKind Kind, bool ForceRefresh);

public enum IngestServerKind { Unspecified, Saved, Twitch }
