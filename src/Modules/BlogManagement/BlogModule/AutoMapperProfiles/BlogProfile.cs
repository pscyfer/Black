using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.DTOs.Command.Blog;
using BlogModule.Services.DTOs.Query.Blog;

namespace BlogModule.AutoMapperProfiles
{
    internal class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog,CreateBlogCommandDto>().ReverseMap();
            CreateMap<Blog,EditBlogCommandDto>().ReverseMap();
            CreateMap<Blog,GetBlogQueryDto>().ReverseMap();
        }
    }
}
