﻿using PaymentApp.Application.Classes.Repositories.Base;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Repositories
{
    public interface ICustomerRepository : IBaseRepostiory<CustomerEntity>
    {
        Task<CustomerEntity> GetByAccountNumberAsync(string number, CancellationToken cancellationToken);
    }
}
