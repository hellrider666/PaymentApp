using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.DTOs;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Queries.GetCustomerByNumber
{
    public class GetCustomerByNumberHandler : BaseHandler, IRequestHandler<GetCustomerByNumberRequest, GetCustomerByNumberResponse>
    {
        public GetCustomerByNumberHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<GetCustomerByNumberResponse> Handle(GetCustomerByNumberRequest request, CancellationToken cancellationToken)
        {
            return await Execute(async unitOfWork =>
            {
                var customer = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(async rep =>
                {
                    return await rep.GetByAccountNumberAsync(request.AccountNumber, cancellationToken);
                });

                return new GetCustomerByNumberResponse { Customer = _mapper.Map<CustomerEntity, CustomerDTO>(customer) };
            });
        }
    }
}
