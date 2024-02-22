using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.CustomerFeatures.CreateCustomer
{
    public class CreateCustomerHandler : BaseHandler, IRequestHandler<CreateCustomerRequest, CreateCustomerResponse>
    {
        public CreateCustomerHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = new CustomerEntity
            {
                Name = request.Name,
                AccountNumber = request.AccountNumber,
                Balance = 0,
                IsActive = true
            };

            _unitOfWork.CustomerRepository.Create(customer);
            await _unitOfWork.SaveChangesAsync();

            return new CreateCustomerResponse { Successful = true };
        }
    }
}
