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
        void AddEntity<T>(T entity) where T : BaseEntity;
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void DisposeContext();
    }
}
