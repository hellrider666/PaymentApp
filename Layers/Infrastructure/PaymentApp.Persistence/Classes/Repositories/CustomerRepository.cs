using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;
using PaymentApp.Persistence.Classes.Context;
using PaymentApp.Persistence.Classes.Repositories.Base;

namespace PaymentApp.Persistence.Classes.Repositories
{
    public class CustomerRepository : BaseRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(IAppDbContext dbContext) : base(dbContext) { }

        public async Task<CustomerEntity> GetByAccountNumberAsync(string number, CancellationToken cancellationToken)
        {
            return await GetInternal().FirstOrDefaultAsync(x => x.AccountNumber == number, cancellationToken);
        }
    }
}
