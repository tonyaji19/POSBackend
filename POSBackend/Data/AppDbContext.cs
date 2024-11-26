using Microsoft.EntityFrameworkCore;
using POSBackend.Models;

namespace POSBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<POSTransaction> POSTransactions { get; set; }

    }

}
