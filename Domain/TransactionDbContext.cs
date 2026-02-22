using expense_tracker_api.Models;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker_api.Domain
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) {}

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
        }
    }
}
