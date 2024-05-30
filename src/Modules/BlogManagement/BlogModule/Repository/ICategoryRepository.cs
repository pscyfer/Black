using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure.Repository;

namespace BlogModule.Repository
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
    }
    public class CategoryRepository : BaseRepository<Category, BlogModuleContext>, ICategoryRepository
    {
        public CategoryRepository(BlogModuleContext context) : base(context)
        {
        }
    }
}
