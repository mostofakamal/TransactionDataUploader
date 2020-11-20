using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Infrastructure
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
            modelBuilder
                .Entity<TransactionEntry>()
                .Property(e => e.TransactionStatusId)
                .HasConversion<int>();

            modelBuilder
                .Entity<TransactionStatus>()
                .Property(e => e.Id)
                .HasConversion<int>();

            modelBuilder
                .Entity<TransactionStatus>().HasData(
                    Enum.GetValues(typeof(TransactionStatusId))
                        .Cast<TransactionStatusId>()
                        .Select(e => new TransactionStatus()
                        {
                            Id = e,
                            Name = e.ToString()
                        })
                );
        }

    }
}
