using Corvus.Nest.Frontend.Services.IServices;
using Corvus.Nest.Frontend.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Corvus.Nest.Frontend.Components.Layout;

public class MainLayoutBase : LayoutComponentBase
{
    [Inject] private IConfiguration _configuration { get; set; } = null!;

    [Inject] private IHttpClientService _httpClient { get; set; } = null!;

    [Inject] private NavigationManager _navigation { get; set; } = null!;

    [Inject] private IJSRuntime _jsRuntime { get; set; } = null!;

    private string _backendApi => _configuration["BackendApi"] ?? string.Empty;

    protected List<GetBlogMenuVM>? Menus { get; set; }

    protected GetAboutVM? About { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var baseTask = base.OnInitializedAsync();

        About = await _httpClient.GetAsync<GetAboutVM>(Path.Combine(_backendApi, "GetAbout"));

        await baseTask;
    }
}