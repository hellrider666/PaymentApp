using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ReplenishmentBalance
{
    public class ReplenishmentBalanceHandler : BaseHandler, IRequestHandler<ReplenishmentBalanceRequest, ReplenishmentBalanceResponse>
    {
        public ReplenishmentBalanceHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ReplenishmentBalanceResponse> Handle(ReplenishmentBalanceRequest request, CancellationToken cancellationToken)
        {
            return await Execute(async unitOfWork =>
            {
                var customer = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>
                (
                     x => x.GetByAccountNumberAsync(request.AccountNumber, cancellationToken)
                );

                unitOfWork.DoWork(() =>
                {
                    customer.Balance += request.Sum;
                });

                return new ReplenishmentBalanceResponse { Successful = true };
            });
        }
    }
}
