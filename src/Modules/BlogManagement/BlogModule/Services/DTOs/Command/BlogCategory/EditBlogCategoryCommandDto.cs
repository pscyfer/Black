namespace BlogModule.Services.DTOs.Command.BlogCategory
{
    public record EditBlogCategoryCommandDto(Guid Id, string Title, string Description, string DesktopImage, string MobileImage, string Slug);
}
