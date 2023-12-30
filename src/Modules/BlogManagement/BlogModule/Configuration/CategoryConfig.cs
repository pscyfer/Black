using BlogModule.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogModule.Configuration
{
    internal class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            string module_Name = "BlogModule_{0}";
            builder.ToTable(string.Format(module_Name, "Category"),"BlogModule");
            builder.HasKey(x => x.Id);


            builder.Property(x=>x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Slug).HasMaxLength(50).IsRequired();
        }
    }
}
