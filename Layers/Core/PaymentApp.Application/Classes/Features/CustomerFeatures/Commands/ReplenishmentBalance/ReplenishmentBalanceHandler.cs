using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ReplenishmentBalance
{
    public class ReplenishmentBalanceHandler : BaseHandler, IRequestHandler<ReplenishmentBalanceRequest, ReplenishmentBalanceResponse>
    {
        public ReplenishmentBalanceHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ReplenishmentBalanceResponse> Handle(ReplenishmentBalanceRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransaction(async () =>
            {
                var customer = await _unitOfWork.CustomerRepository.GetByAccountNumberAsync(request.AccountNumber, cancellationToken);

                customer.Balance += request.Sum;

                return new ReplenishmentBalanceResponse { Successful = true };
            });
        }
    }
}
