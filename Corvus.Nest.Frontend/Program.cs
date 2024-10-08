using Corvus.Nest.Frontend.Components;
using Corvus.Nest.Frontend.Services;
using Corvus.Nest.Frontend.Services.IServices;

namespace Corvus.Nest.Frontend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;

        builder.WebHost.UseStaticWebAssets();

        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services.AddHttpClient();

        builder.Services.AddScoped<IHttpClientService, HttpClientService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
