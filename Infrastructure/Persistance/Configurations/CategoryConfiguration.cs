using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasQueryFilter(c => c.IsDeleted == false);

            builder.ToTable("Categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // ya da Cascade

            builder.HasData(
                new Category("Tükenmez Kalemler") { Id = 1 },
                new Category("Kurşun Kalemler") { Id = 2 },
                new Category("Dolma Kalemler") { Id = 3 },
                new Category("Promosyon Kalemler") { Id = 4 },
                new Category("Lüks Kalemler") { Id = 5 }
            );
        }
    }
}
