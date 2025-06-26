namespace AshwellForge.Admin.Services;

public static class HttpClientExtension
{
    public static async Task<HttpResponseMessage> PerformRequest(this HttpClient client, string uri)
    {
        try
        {
            return await client.GetAsync(uri);
        }
        catch (Exception)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}
