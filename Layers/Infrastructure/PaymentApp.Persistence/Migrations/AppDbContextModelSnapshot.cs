﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentApp.Persistence.Classes.Context;

#nullable disable

namespace PaymentApp.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaymentApp.Domain.Entities.CustomerEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("AccountNumber")
                        .IsUnique()
                        .HasDatabaseName("Customer_Number_Unique_Index");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("PaymentApp.Domain.Entities.TransactionEntity", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<DateTime>("ExecutingDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("RecipientCustomerId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("SenderCustomerId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<string>("TransactionNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("RecipientCustomerId");

                    b.HasIndex("SenderCustomerId");

                    b.HasIndex("TransactionNumber")
                        .IsUnique()
                        .HasDatabaseName("Transactions_Number_Unique_Index");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("PaymentApp.Domain.Entities.TransactionEntity", b =>
                {
                    b.HasOne("PaymentApp.Domain.Entities.CustomerEntity", "RecipientCustomer")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("RecipientCustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("RecipientCustomerId");

                    b.HasOne("PaymentApp.Domain.Entities.CustomerEntity", "SenderCustomer")
                        .WithMany("SentTransactions")
                        .HasForeignKey("SenderCustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("SenderCustomerId");

                    b.Navigation("RecipientCustomer");

                    b.Navigation("SenderCustomer");
                });

            modelBuilder.Entity("PaymentApp.Domain.Entities.CustomerEntity", b =>
                {
                    b.Navigation("ReceivedTransactions");

                    b.Navigation("SentTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
