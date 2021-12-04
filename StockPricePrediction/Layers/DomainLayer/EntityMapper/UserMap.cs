using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_userid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("INT");
            builder.Property(x => x.Firstname)
                .HasColumnName("firstname")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.Lastname)
                .HasColumnName("lastname")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("VARCHAR(50)")
                .IsRequired();
            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("VARCHAR(50)")
                .IsRequired();
        }
    }
}