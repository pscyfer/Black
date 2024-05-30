using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;

namespace BlogModule.Repository
{
    public interface IBlogRepository : IBaseRepository<Blog>
    {
    }
    public class BlogRepository : BaseRepository<Blog, BlogModuleContext>, IBlogRepository
    {
        public BlogRepository(BlogModuleContext context) : base(context)
        {
        }
    }
}
