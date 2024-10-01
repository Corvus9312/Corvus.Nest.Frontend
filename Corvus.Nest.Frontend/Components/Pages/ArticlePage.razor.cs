using Corvus.Nest.Frontend.Models.DAL;
using Corvus.Nest.Frontend.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Web;

namespace Corvus.Nest.Frontend.Components.Pages;

public class ArticlePageBase : CusComponentBase
{
    [Parameter] public Guid ArticleID { get; set; }

    protected Article? Article { get; set; }

    protected List<GetArticlesVM>? Articles { get; set; }

    protected GetArticlesVM? PreviousArticle { get; set; }

    protected GetArticlesVM? NextArticle { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await SetPageParameters();
    }

    private async Task<Article?> GetArticle()
    {
        UriBuilder builder = new(Path.Combine(BackendApi, "GetArticle"));

        var query = HttpUtility.ParseQueryString(builder.Query);
        query.Add("id", $"{ArticleID}");

        builder.Query = query.ToString();
        var url = builder.ToString();

        return await HttpClient.GetAsync<Article>(url);
    }

    private async Task<List<GetArticlesVM>?> GetArticles()
    {
        UriBuilder builder = new(Path.Combine(BackendApi, "GetArticles"));

        var query = HttpUtility.ParseQueryString(builder.Query);
        query.Add("categoryID", $"{Article?.Category}");

        builder.Query = query.ToString();

        var url = builder.ToString();

        return (await HttpClient.GetAsync<List<GetArticlesVM>>(url))?.OrderBy(x => x.Sort).ToList();
    }

    private async Task SetPageParameters()
    {
        Article = await GetArticle();

        if (Article is null)
        {
            await JsRuntime.InvokeVoidAsync("alert", "查無此文章，頁面導回文章清單。");
            Navigation.NavigateTo("/articles");
            return;
        }

        Articles = await GetArticles();
        PreviousArticle = Articles?.LastOrDefault(x => x.Sort < Article.Sort);
        NextArticle = Articles?.FirstOrDefault(x => x.Sort > Article.Sort);

        StateHasChanged();
    }
}