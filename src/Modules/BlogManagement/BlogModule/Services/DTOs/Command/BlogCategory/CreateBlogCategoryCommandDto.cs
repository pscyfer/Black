namespace BlogModule.Services.DTOs.Command.BlogCategory
{
    public record CreateBlogCategoryCommandDto(string Title, string Description, string DesktopImage, string MobileImage, string Slug);
}
