using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_id");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("INT");
            builder.Property(x => x.Firstname)
                .HasColumnName("firstname")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();            
            builder.Property(x => x.Lastname)
                .HasColumnName("lastname")
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
        }
    }
}