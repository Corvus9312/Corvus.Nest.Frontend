using Microsoft.JSInterop;
using System.Web;
using System.Text;
using Corvus.Nest.Frontend.Extensions;
using Corvus.Nest.Frontend.Services.IServices;

namespace Corvus.Nest.Frontend.Services;

public class HttpClientService : IHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public HttpClientService(IJSRuntime jsruntime)
    {
        _httpClient = new(
            new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true,
                AllowAutoRedirect = false
            });

        _jsRuntime = jsruntime;
    }

    public async Task<string> GetStringAsync(string apiUrl, Dictionary<string, string>? queryStr = null)
    {
        var builder = new UriBuilder(apiUrl);

        if (queryStr is not null)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var item in queryStr)
                query.Add(item.Key, item.Value);

            builder.Query = query.ToString();
        }

        var apiUri = builder.ToString();

        return await _httpClient.GetStringAsync(apiUri);
    }

    public async Task<T?> GetAsync<T>(string apiUrl, Dictionary<string, string>? queryStr = null) where T : new()
    {
        T? result = default;

        var builder = new UriBuilder(apiUrl);

        if (queryStr is not null)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);

            foreach (var item in queryStr)
                query.Add(item.Key, item.Value);

            builder.Query = query.ToString();
        }

        var apiUri = builder.ToString();

        var res = await _httpClient.GetAsync(apiUri);

        if (res.IsSuccessStatusCode)
            result = await res.Content.ReadFromJsonAsync<T>();

        return result;
    }

    public async Task<T?> PatchAsync<T>(string apiUrl, object? requestModel) where T : new()
    {
        T? result = default;

        var req = new StringContent(JsonConvert.Serialize(requestModel), Encoding.UTF8, "application/json");

        var res = await _httpClient.PatchAsync(apiUrl, req);

        if (res.IsSuccessStatusCode)
            result = await res.Content.ReadFromJsonAsync<T>();

        return result;
    }

    public async Task<T?> PostAsync<T>(string apiUrl, object? requestModel) where T : new()
    {
        T? result = default;

        var res = await _httpClient.PostAsJsonAsync(apiUrl, requestModel);

        if (res.IsSuccessStatusCode)
            result = await res.Content.ReadFromJsonAsync<T>();

        return result;
    }
}