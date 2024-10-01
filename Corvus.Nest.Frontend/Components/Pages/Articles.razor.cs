using Corvus.Nest.Frontend.Models.DAL;
using Corvus.Nest.Frontend.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Web;

namespace Corvus.Nest.Frontend.Components.Pages;

public class ArticlesBase : CusComponentBase
{
    public List<GetArticlesVM>? GetArticles { get; set; }

    [Parameter] public Guid? Category { get; set; } = null;

    protected Category? GetCategory { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        UriBuilder builder = new(Path.Combine(BackendApi, "GetArticles"));

        if (Category != null)
        {
            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("categoryID", $"{Category}");

            builder.Query = query.ToString();
        }

        var url = builder.ToString();

        GetArticles = await HttpClient.GetAsync<List<GetArticlesVM>>(url);

        GetCategory = GetArticles?.FirstOrDefault(x => x.ID.Equals(Category))?.CategoryNavigation;

        StateHasChanged();
    }
}