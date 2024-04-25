using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PaymentApp.Application.Classes.Abstract.Interfaces;

namespace PaymentApp.Application.Classes.Managers.Request
{
    public class RequestManager : IRequestManager
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        public RequestManager(IMediator mediator, IServiceProvider serviceProvider)
        {
            _mediator = mediator;

            _serviceProvider = serviceProvider;
        }
        public async Task<TResponse> Send<TResponse>(IAppRequest<TResponse> request)
        {
            return await _mediator.Send(request);
        }
    }
}
