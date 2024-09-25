namespace Corvus.Nest.Frontend.ViewModels;

public class GetAboutVM
{
    public string Name { get; set; } = null!;

    public string? Avatar { get; set; }

    public string Introduce { get; set; } = null!;

    public List<SocialMedia> SocialMedias { get; set; } = new();
}

public class SocialMedia
{
    public string Media { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public string HyperLink { get; set; } = null!;

    public int Sort { get; set; }
}