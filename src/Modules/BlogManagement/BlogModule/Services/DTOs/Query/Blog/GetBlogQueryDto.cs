namespace BlogModule.Services.DTOs.Query.Blog
{
    public record GetBlogQueryDto(Guid Id, string Title, string Abstract, string Descriptions, string Author, string Slug, string DesktopImage, string MobileImage);

}
