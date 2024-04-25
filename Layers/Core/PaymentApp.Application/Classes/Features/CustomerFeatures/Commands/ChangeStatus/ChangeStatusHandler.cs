using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.ChangeStatus
{
    public class ChangeStatusHandler : BaseHandler, IRequestHandler<ChangeStatusRequest, ChangeStatusResponse>
    {
        public ChangeStatusHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ChangeStatusResponse> Handle(ChangeStatusRequest request, CancellationToken cancellationToken)
        {
            return await Execute(async unitOfWork =>
            {
                var customer = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>
                (
                    rep => rep.GetByAccountNumberAsync(request.AccountNumber, cancellationToken)
                );

                 unitOfWork.DoWork(() =>
                {
                    customer.IsActive = request.IsActive;
                });

                return new ChangeStatusResponse { Successful = true };
            });
        }
    }
}
