using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // burada class yapılandırılması yapılmaktadır.
        builder.HasKey(x => x.ProductId);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x=>x.Stock).IsRequired();
    }
}