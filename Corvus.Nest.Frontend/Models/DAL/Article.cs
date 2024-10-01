namespace Corvus.Nest.Frontend.Models.DAL;

public class Article
{
    public Guid ID { get; set; }

    public Guid Category { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public string ArticleContent { get; set; } = null!;

    public int Sort { get; set; }

    public DateTime CreateTime { get; set; }

    public Category CategoryNavigation { get; set; } = null!;
}