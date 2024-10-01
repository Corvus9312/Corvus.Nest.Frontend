using Corvus.Nest.Frontend.Services.IServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Corvus.Nest.Frontend.Components;

public class CusComponentBase : ComponentBase
{
    [Inject] public IConfiguration Configuration { get; set; } = null!;

    [Inject] public IHttpClientService HttpClient { get; set; } = null!;

    [Inject] public NavigationManager Navigation { get; set; } = null!;

    [Inject] public IJSRuntime JsRuntime { get; set; } = null!;

    public string BackendApi => Configuration["BackendApi"] ?? string.Empty;
}