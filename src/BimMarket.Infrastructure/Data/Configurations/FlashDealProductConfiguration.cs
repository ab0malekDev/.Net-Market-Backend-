using BimMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BimMarket.Infrastructure.Data.Configurations;

public class FlashDealProductConfiguration : IEntityTypeConfiguration<FlashDealProduct>
{
    public void Configure(EntityTypeBuilder<FlashDealProduct> b)
    {
        b.ToTable("FlashDealProducts");
        b.HasKey(x => x.Id);
        b.HasIndex(x => new { x.FlashDealId, x.ProductId }).IsUnique();
        b.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Cascade);
    }
}
