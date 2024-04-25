using AutoMapper;
using PaymentApp.Application.Classes.Repositories;

namespace PaymentApp.Application.Classes.Abstract
{
    public abstract class BaseHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;

            _mapper = mapper;
        }

        protected async Task<TResult> Execute<TResult>(Func<IUnitOfWork, Task<TResult>> action)
        {
             return await action(_unitOfWork);
        }

        protected TResult Execute<TResult>(Func<IUnitOfWork, TResult> action)
        {
             return action(_unitOfWork);
        }

        protected void Execute(Action<IUnitOfWork> action) 
        {
             action(_unitOfWork);
        }
    }
}
