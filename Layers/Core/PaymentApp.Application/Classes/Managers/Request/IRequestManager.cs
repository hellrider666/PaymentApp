using PaymentApp.Application.Classes.Abstract.Interfaces;

namespace PaymentApp.Application.Classes.Managers.Request
{
    public interface IRequestManager
    {
        Task<TResponse> Send<TResponse>(IAppRequest<TResponse> request);
    }
}
