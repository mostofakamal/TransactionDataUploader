using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactionDataUploader.Core.Domain.Models;

namespace TransactionDataUploader.Core.Infrastructure.Persistence.EfEntityConfigurations
{
    public class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder
                .Property(e => e.Id)
                .HasConversion<int>();

            builder.HasData(
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