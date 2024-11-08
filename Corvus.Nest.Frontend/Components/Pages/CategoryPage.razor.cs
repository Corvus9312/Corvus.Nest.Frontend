using Corvus.Nest.Frontend.Models.DAL;

namespace Corvus.Nest.Frontend.Components.Pages;

public class CategoryPageBase : CusComponentBase
{
    protected List<Category>? Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        UriBuilder builder = new(Path.Combine(BackendApi, "GetCategories"));

        var url = builder.ToString();

        Categories = await HttpClient.GetAsync<List<Category>>(url);

        StateHasChanged();
    }
}