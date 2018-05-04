using Microsoft.EntityFrameworkCore;
using TheBookCave.Data.EntityModels;
using TheBookCave.Data.EntityModels.BookEntityModel;
using TheBookCave.Data.EntityModels.OrderEntityModel;
using TheBookCave.Data.EntityModels.RatingEntityModel;

namespace TheBookCave.Data
{
    public class DataContext : DbContext
    {
        public DbSet<BookEntityModel> Books { get; set; }
        public DbSet<OrderEntityModel> Orders { get; set; }
        public DbSet<RatingEntityModel> Ratings { get; set; }
        public DbSet<UserEntityModel> Users { get; set; }

        public DbSet<OrderBookConnectionEntityModel> OrderBookConnections { get; set; }
        public DbSet<UserBookCartConnectionEntityModel> UserBookCartConnections { get; set; }
        public DbSet<UserBookWishlistConnectionEntityModel> UserBookWishlistConnections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //When we get the SQL server, go to database part1 14:30 min.
            optionsBuilder
                .UseSqlServer(
                    "Server=tcp:verklegt2.database.windows.net,1433;Initial Catalog=VLN2_2018_H06;Persist Security Info=False;User ID=VLN2_2018_H06_usr;Password=grea+Puma68;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}