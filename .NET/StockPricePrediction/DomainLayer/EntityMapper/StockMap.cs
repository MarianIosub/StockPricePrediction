using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DomainLayer
{
    public class StockMap:IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_stockid");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("INT");
            builder.Property(x => x.Title)
                .HasColumnName("title")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
        }
    }
}