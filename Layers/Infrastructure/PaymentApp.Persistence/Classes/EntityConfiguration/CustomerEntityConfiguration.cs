using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentApp.Domain.Entities;
using PaymentApp.Persistence.Classes.EntityConfiguration.Extensions;

namespace PaymentApp.Persistence.Classes.EntityConfiguration
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder
                .ToTable("Customers");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder
                .HasIndex(x => x.AccountNumber)
                .SetUniqueIndex("Customer", "Number");

            builder
                .Property(x => x.AccountNumber)
                .HasMaxLength(16)
                .IsRequired();

            builder
                .Property(x => x.Balance)
                .AsDecimal()
                .IsRequired();

            builder
                .Property(x => x.IsActive)
                .IsRequired();
        }
    }
}
