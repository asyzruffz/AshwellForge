using AshwellForge.Admin.Utils;
using System.Web;

namespace AshwellForge.Admin.Models;

public record GetStreamsParam(int Page, int PageSize, string? Filter)
{
    public string AppendToUri(string Uri)
    {
        var query = UrlQueryString.Create()
            .Set("page", Page.ToString())
            .Set("pageSize", PageSize.ToString());
        if (!string.IsNullOrEmpty(Filter))
            query.Set("filter", Filter);
        return $"{Uri}{query.Build()}";
    }
}
