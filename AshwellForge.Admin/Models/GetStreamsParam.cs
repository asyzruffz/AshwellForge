using AshwellForge.Admin.Utils;

namespace AshwellForge.Admin.Models;

public record GetStreamsParam(int Page, int PageSize, string? Filter)
{
    public string ForUri()
    {
        var query = UrlQueryString.Create()
            .Set("page", Page.ToString())
            .Set("pageSize", PageSize.ToString());
        if (!string.IsNullOrEmpty(Filter))
            query.Set("filter", Filter);
        return query.Build();
    }
}
