namespace BlogModule.Services.DTOs.Query.BlogCategory
{
    public class GetBlogCategoryQueryDto
    {
        public Guid Id { get; set; }
        public int Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string DesktopImage { get; set; }
        public string MobileImage { get; set; }
    }
}
