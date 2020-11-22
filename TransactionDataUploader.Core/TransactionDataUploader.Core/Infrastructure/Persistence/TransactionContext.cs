using Microsoft.EntityFrameworkCore;
using TransactionDataUploader.Core.Domain.Models;
using TransactionDataUploader.Core.Infrastructure.Persistence.EfEntityConfigurations;

namespace TransactionDataUploader.Core.Infrastructure.Persistence
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {
        }
        
        public DbSet<TransactionEntry> TransactionEntries { get; set; }

        public DbSet<TransactionStatus> TransactionStatuses { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionEntryConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionStatusConfiguration());
        }

    }
}
