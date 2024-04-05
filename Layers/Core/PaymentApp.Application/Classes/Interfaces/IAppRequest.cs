using MediatR;

namespace PaymentApp.Application.Classes.Interfaces
{
    public interface IAppRequest<T> : IRequest<T>
    {

    }
}
