using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.DTOs.Command.BlogCategory;
using BlogModule.Services.DTOs.Query.BlogCategory;

namespace BlogModule.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CreateBlogCategoryCommandDto>().ReverseMap();
            CreateMap<Category, EditBlogCategoryCommandDto>().ReverseMap();
            CreateMap<Category, GetBlogCategoryQueryDto>().ReverseMap();
        }
    }
}
