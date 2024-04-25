using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PaymentApp.Domain.Entities;
using PaymentApp.Domain.Entities.Base;
using PaymentApp.Persistence.Classes.EntityConfiguration;

namespace PaymentApp.Persistence.Classes.Context
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<CustomerEntity> CustomerEntities { get; set; }
        public DbSet<TransactionEntity> TransactionEntities { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerEntityConfiguration).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
        }

        public void DisposeContext()
        {
            base.Dispose();
        }

        public DbSet<T> GetDbSet<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public int SaveChangesSync()
        {
            return base.SaveChanges();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await base.Database.BeginTransactionAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return base.Database.BeginTransaction();
        }
    }
}
