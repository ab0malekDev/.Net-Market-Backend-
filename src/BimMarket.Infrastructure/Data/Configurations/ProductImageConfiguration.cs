using BimMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BimMarket.Infrastructure.Data.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> b)
    {
        b.ToTable("ProductImages");
        b.HasKey(x => x.Id);
        b.Property(x => x.Url).HasMaxLength(1000);
        b.Property(x => x.ThumbnailUrl).HasMaxLength(1000);
    }
}
