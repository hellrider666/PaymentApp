using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.PaymentApi.Classes.Responses;
using System.Net;

namespace PaymentApp.PaymentApi.Controllers.Base
{
    [Route("api/[controller]/[action]")]
    [ApiController]    
    [ProducesErrorResponseType(typeof(ValidationResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.ServiceUnavailable)]
    public class BaseApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BaseApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> Execute<TRequest, TResponse>(IRequest<TResponse> request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
