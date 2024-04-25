using FluentValidation;
using MediatR;
using PaymentApp.Application.Classes.Interfaces;

namespace PaymentApp.Application.Classes.Managers.Request
{
    public class RequestManager : IRequestManager
    {
        private readonly IMediator _mediator;
        public RequestManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IResponse> Send<IResponse>(IAppRequest<IResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
}
