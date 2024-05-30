using BlogModule.AutoMapperProfiles;
using BlogModule.Repository;
using BlogModule.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogModule
{
    public static class BlogManagementBootstrapper
    {
        public static IServiceCollection ConfigureBlogManagement(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BlogModuleContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            services.AddTransient<IBlogModuleService, BlogModuleService>();

            services.AddAutoMapper(typeof(BlogProfile).Assembly);
            return services;
        }
    }
}
