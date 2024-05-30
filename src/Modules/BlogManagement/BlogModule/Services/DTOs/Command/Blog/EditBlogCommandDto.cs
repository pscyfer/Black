namespace BlogModule.Services.DTOs.Command.Blog;

public record EditBlogCommandDto(Guid Id, string Title,string Abstract,string Descriptions,string Author,string Slug,string DesktopImage,string MobileImage);
