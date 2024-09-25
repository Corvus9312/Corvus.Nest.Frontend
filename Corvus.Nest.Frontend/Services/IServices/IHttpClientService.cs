namespace Corvus.Nest.Frontend.Services.IServices;

public interface IHttpClientService
{
    Task<string> GetStringAsync(string apiUrl, Dictionary<string, string>? queryStr = null);

    Task<T?> GetAsync<T>(string apiUrl, Dictionary<string, string>? queryStr = null) where T : new();

    Task<T?> PostAsync<T>(string apiUrl, object? requestModel) where T : new();

    Task<T?> PatchAsync<T>(string apiUrl, object? requestModel) where T : new();
}
