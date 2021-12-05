using DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserStocks>().HasKey(sc => new
            {
                sc.UserId, sc.StockId
            });
            modelBuilder.Entity<UserStocks>()
                .HasOne<User>(sc => sc.User)
                .WithMany(s => s.UserStocks)
                .HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<UserStocks>()
                .HasOne<Stock>(sc => sc.Stock)
                .WithMany(s => s.UsersStocks)
                .HasForeignKey(sc => sc.StockId);

            modelBuilder.ApplyConfiguration(new StockMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new CommentMap());


            base.OnModelCreating(modelBuilder);
        }
    }
}