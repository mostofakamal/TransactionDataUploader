using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Infrastructure.Persistence.EfEntityConfigurations
{
    public class TransactionEntryConfiguration : IEntityTypeConfiguration<TransactionEntry>
    {
        public void Configure(EntityTypeBuilder<TransactionEntry> builder)
        {
            builder.Property(x => x.TransactionId).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(4);
            builder.Property(e => e.TransactionStatusId)
                .HasConversion<int>().IsRequired();

        }
    }
}
