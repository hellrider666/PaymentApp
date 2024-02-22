using MediatR;
using PaymentApp.Application.Classes.Features.CustomerFeatures.CreateCustomer;
using PaymentApp.Application.Classes.Repositories;

namespace PaymentApp.Application.Classes.Abstract
{
    public abstract class BaseHandler
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
