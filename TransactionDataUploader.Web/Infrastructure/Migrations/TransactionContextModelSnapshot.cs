﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionDataUploader.Core.Infrastructure.Persistence;

namespace TransactionDataUploader.Web.Infrastructure.Migrations
{
    [DbContext(typeof(TransactionContext))]
    partial class TransactionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TransactionDataUploader.Core.Domain.Models.TransactionEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TransactionStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionStatusId");

                    b.ToTable("TransactionEntries");
                });

            modelBuilder.Entity("TransactionDataUploader.Core.Domain.Models.TransactionStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "A"
                        },
                        new
                        {
                            Id = 2,
                            Name = "R"
                        },
                        new
                        {
                            Id = 3,
                            Name = "D"
                        });
                });

            modelBuilder.Entity("TransactionDataUploader.Core.Domain.Models.TransactionEntry", b =>
                {
                    b.HasOne("TransactionDataUploader.Core.Domain.Models.TransactionStatus", "TransactionStatus")
                        .WithMany()
                        .HasForeignKey("TransactionStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransactionStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
