using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Entities.Base;

namespace PaymentApp.Persistence.Classes.Context
{
    public interface IAppDbContext
    {
        DbSet<CustomerEntity> CustomerEntities { get; }
        DbSet<TransactionEntity> TransactionEntities { get; }
        DbSet<T> GetDbSet<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();
        int SaveChangesSync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IDbContextTransaction BeginTransaction();
        void DisposeContext();
    }
}
