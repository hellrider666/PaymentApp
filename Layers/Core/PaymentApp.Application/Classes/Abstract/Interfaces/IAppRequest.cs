using MediatR;

namespace PaymentApp.Application.Classes.Abstract.Interfaces
{
    public interface IAppRequest<TResponse> : IRequest<TResponse>
    {
    }
}
