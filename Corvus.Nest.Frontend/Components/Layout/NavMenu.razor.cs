using Corvus.Nest.Frontend.Services.IServices;
using Corvus.Nest.Frontend.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Corvus.Nest.Frontend.Components.Layout;

public class NavMenuBase : ComponentBase
{
    [Inject] private IConfiguration _configuration { get; set; } = null!;

    [Inject] private IHttpClientService _httpClient { get; set; } = null!;

    private string _backendApi => _configuration["BackendApi"] ?? string.Empty;

    protected List<GetBlogMenuVM>? Menus { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var baseTask = base.OnInitializedAsync();

        Menus = await _httpClient.GetAsync<List<GetBlogMenuVM>>(Path.Combine(_backendApi, "GetBlogMenus"));

        StateHasChanged();

        await baseTask;
    }
}