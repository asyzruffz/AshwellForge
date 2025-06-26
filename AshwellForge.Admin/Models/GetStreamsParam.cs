using System.Web;

namespace AshwellForge.Admin.Models;

public record GetStreamsParam(int Page, int PageSize, string? Filter)
{
    public string IncludeToUri(string Uri)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        query["page"] = Page.ToString();
        query["pageSize"] = PageSize.ToString();
        if (!string.IsNullOrEmpty(Filter)) query["filter"] = Filter;
        return $"{Uri}?{query.ToString()}";
    }
}
