using BlogModule.Configuration;
using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogModule
{
    public class BlogModuleContext : DbContext
    {
        public BlogModuleContext(DbContextOptions<BlogModuleContext> options)
                    : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

