using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentApp.Domain.Entities;
using PaymentApp.Persistence.Classes.EntityConfiguration.Extensions;

namespace PaymentApp.Persistence.Classes.EntityConfiguration
{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder
                .ToTable("Transactions");

            builder
                .HasIndex(x => x.TransactionNumber)
                .SetUniqueIndex("Transactions", "Number");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.SenderCustomer)
                .WithMany(x => x.SentTransactions)
                .HasConstraintName("SenderCustomerId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .HasOne(x => x.RecipientCustomer)
                .WithMany(x => x.ReceivedTransactions)
                .HasConstraintName("RecipientCustomerId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder
                .Property(x => x.TransactionNumber)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(x => x.Sum)
                .AsDecimal()
                .IsRequired();

            builder
                .Property(x => x.ExecutingDateTime)
                .IsRequired();
        }
    }
}
