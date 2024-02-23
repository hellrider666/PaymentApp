using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ChangeStatus
{
    public class ChangeStatusHandler : BaseHandler, IRequestHandler<ChangeStatusRequest, ChangeStatusResponse>
    {
        public ChangeStatusHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ChangeStatusResponse> Handle(ChangeStatusRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransaction(async () => 
            {
                var customer = await _unitOfWork.CustomerRepository.GetByAccountNumberAsync(request.AccountNumber, cancellationToken);

                customer.IsActive = request.IsActive;
                _unitOfWork.CustomerRepository.Update(customer);

                return new ChangeStatusResponse { Successful = true };
            });           
        }
    }
}
