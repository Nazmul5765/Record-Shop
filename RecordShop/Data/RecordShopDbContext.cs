using Microsoft.EntityFrameworkCore;
using RecordShop.Models;

namespace RecordShop.Data
{
    public class RecordShopDbContext : DbContext
    {
        public RecordShopDbContext(DbContextOptions<RecordShopDbContext> options)
            : base(options)
        {

        }

        public DbSet<Album> Albums { get; set; }
    }
}
