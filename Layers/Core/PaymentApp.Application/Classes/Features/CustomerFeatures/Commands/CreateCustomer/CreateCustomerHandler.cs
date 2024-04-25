using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.Commands.CreateCustomer
{
    public class CreateCustomerHandler : BaseHandler, IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        public CreateCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            return Execute(unitOfWork =>
            {
                var user = new CustomerEntity
                {
                    Name = request.Name,
                    AccountNumber = request.AccountNumber,
                    Balance = 0,
                    IsActive = true
                };

                unitOfWork.DoWork<ICustomerRepository, CustomerEntity>(rep =>
                {
                    rep.Create(user);
                });

                return new CreateCustomerResponse { Successful = true };
            });      
       }
    }
}
