using AutoMapper;
using PaymentApp.Application.Classes.Repositories;

namespace PaymentApp.Application.Classes.Abstract
{
    public abstract class BaseHandler
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }
    }
}
