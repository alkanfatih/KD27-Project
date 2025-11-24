using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // veya Cascade

            builder.HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);

            builder.Property(p => p.Discount).HasPrecision(18, 2);

            builder.HasOne(p => p.Detail)
                   .WithOne(d => d.Product)
                   .HasForeignKey<ProductDetail>(d => d.ProductId);


            builder.HasData(
                new Product("Pilot G2 Tükenmez Kalem", 34.90m, 120, 1, "Yumuşak yazım konforu") { Id = 1 },
                new Product("Parker IM Dolma Kalem", 449.00m, 15, 3, "Paslanmaz çelik gövde") { Id = 2 },
                new Product("Faber-Castell Grip Kurşun Kalem", 9.99m, 500, 2, "Ergonomik tutuş") { Id = 3 },
                new Product("Özel Logo Promosyon Kalem", 5.50m, 1000, 4, "Toplu alımda indirimli") { Id = 4 },
                new Product("Scrikss Noble 35 Dolma Kalem", 599.00m, 20, 3, "Altın kaplama uç ve şık tasarım") { Id = 5 },
                new Product("Faber-Castell Click Tükenmez Kalem", 12.50m, 300, 1, "Click mekanizmalı") { Id = 6 },
                new Product("Pritt HB Kurşun Kalem", 3.90m, 800, 2, "Klasik ahşap kurşun kalem") { Id = 7 },
                new Product("Bic Soft Feel Tükenmez Kalem", 6.75m, 450, 1, "Rahat tutuş ve canlı yazım") { Id = 8 },
                new Product("Rotring 500 Teknik Kalem", 179.90m, 50, 3, "Teknik çizim için özel") { Id = 9 },
                new Product("Lamy Safari Dolma Kalem", 399.00m, 25, 3, "Modern tasarım, kaliteli uç") { Id = 10 },
                new Product("Maped Kurşun Kalem HB", 4.50m, 600, 2, "Derslerde ideal") { Id = 11 },
                new Product("Uniball Signo Jel Kalem", 17.90m, 200, 1, "Suya dayanıklı jel mürekkep") { Id = 12 },
                new Product("Kurum İçi Promosyon Kalemi", 2.99m, 1500, 4, "Toplu alıma özel indirimli") { Id = 13 },
                new Product("Lüks Metal Gövdeli Promosyon Kalem", 14.50m, 700, 4, "Logo baskıya uygundur") { Id = 14 },
                new Product("Papermate InkJoy Tükenmez Kalem", 9.95m, 380, 1, "Yumuşak ve renkli yazım") { Id = 15 },
                new Product("Stabilo EasyGraph Kurşun Kalem", 6.75m, 520, 2, "Çocuklar için ergonomik") { Id = 16 },
                new Product("Online Switch Dolma Kalem", 229.00m, 18, 3, "Renkli gövde, kartuşlu sistem") { Id = 17 },
                new Product("Şirket Tanıtım Kalemi (Promosyon)", 3.75m, 1200, 4, "Sergiler için ideal promosyon") { Id = 18 },
                new Product("Reklam Ajansı Promosyon Kalemi", 4.99m, 950, 4, "Uygun fiyatlı promosyon ürünü") { Id = 19 },
                new Product("Pelikan Classic 200 Dolma Kalem", 699.00m, 10, 3, "Koleksiyonluk klasik seri") { Id = 20 }
            );

        }
    }
}
