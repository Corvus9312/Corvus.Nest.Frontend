namespace Corvus.Nest.Frontend.Models.DAL;

public class Category
{
    public Guid ID { get; set; }

    public string Title { get; set; } = null!;

    public IEnumerable<Article> Articles { get; set; } = [];
}