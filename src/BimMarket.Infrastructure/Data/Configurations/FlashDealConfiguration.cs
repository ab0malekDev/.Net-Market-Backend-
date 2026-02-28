using BimMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BimMarket.Infrastructure.Data.Configurations;

public class FlashDealConfiguration : IEntityTypeConfiguration<FlashDeal>
{
    public void Configure(EntityTypeBuilder<FlashDeal> b)
    {
        b.ToTable("FlashDeals");
        b.HasKey(x => x.Id);
        b.Property(x => x.Title).HasMaxLength(200);
        b.HasMany(x => x.Products).WithOne(x => x.FlashDeal).HasForeignKey(x => x.FlashDealId).OnDelete(DeleteBehavior.Cascade);
    }
}
