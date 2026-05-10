using Microsoft.EntityFrameworkCore;
using RecordShop.Models;

namespace RecordShop.Data
{
    public class RecordShopDbContext : DbContext
    {        
        public DbSet<Album> Albums { get; set; }
        public RecordShopDbContext(DbContextOptions<RecordShopDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        { modelBuilder.Entity<Album>().Property(a => a.Price).HasPrecision(18, 2); }
    }
}
