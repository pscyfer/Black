using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogModule.Configuration
{
    internal class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            string module_Name = "BlogModule_{0}";
            builder.ToTable(string.Format(module_Name,"Blogs"), "BlogModule");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Slug).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Abstract).HasMaxLength(300).IsRequired();
        }
    }
}
