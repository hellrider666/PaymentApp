using PaymentApp.Application.Classes.Interfaces;

namespace PaymentApp.Application.Classes.Managers.Request
{
    public interface IRequestManager
    {
        Task<IResponse> Send<IResponse>(IAppRequest<IResponse> request);
    }
}
