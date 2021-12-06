using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainLayer
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_commentid");

            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("id")
                .HasColumnType("INT");
            builder.Property(x => x.Message)
                .HasColumnName("message")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            builder.Property(x => x.Author)
                .HasColumnName("author")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
        }
    }
}