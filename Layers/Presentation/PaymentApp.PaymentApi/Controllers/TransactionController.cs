using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentApp.Application.Classes.Features.TransactionFeatures.Commands.ExecuteTransaction;
using PaymentApp.Application.Classes.Features.TransactionFeatures.Queries.GetTransactions;
using PaymentApp.PaymentApi.Controllers.Base;
using System.Net;

namespace PaymentApp.PaymentApi.Controllers
{
    public class TransactionController : BaseApiController
    {
        public TransactionController(IMediator mediator) : base(mediator) { }

        [HttpPost(Name = "ExecuteTransaction"), ProducesResponseType(typeof(ExecuteTransactionResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ExecuteTransaction([FromHeader] string API_KEY, [FromBody] ExecuteTransactionRequest request)
        {
            return await Execute<ExecuteTransactionRequest, ExecuteTransactionResponse>(request);
        }

        [HttpGet(Name = "GetTransactions"), ProducesResponseType(typeof(GetTransactionsResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTransactions([FromHeader] string API_KEY, [FromQuery] GetTransactionsRequest request)
        {
            return await Execute<GetTransactionsRequest, GetTransactionsResponse>(request);
        }
    }
}
