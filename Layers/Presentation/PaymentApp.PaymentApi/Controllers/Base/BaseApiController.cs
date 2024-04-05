using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.Classes.Interfaces;
using PaymentApp.Application.Classes.Managers.Request;
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
        private readonly IRequestManager _requestManager;

        public BaseApiController(IRequestManager requestManager)
        {
            _requestManager = requestManager;
        }

        protected async Task<IActionResult> Execute<TRequest, TResponse>(IAppRequest<TResponse> request)
        {
            var response = await _requestManager.Send(request);

            return Ok(response);
        }
    }
}
