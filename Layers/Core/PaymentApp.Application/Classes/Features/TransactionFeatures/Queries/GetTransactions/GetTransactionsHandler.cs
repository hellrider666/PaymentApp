using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.DTOs;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Queries.GetTransactions
{
    public class GetTransactionsHandler : BaseHandler, IRequestHandler<GetTransactionsRequest, GetTransactionsResponse>
    {
        public GetTransactionsHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<GetTransactionsResponse> Handle(GetTransactionsRequest request, CancellationToken cancellationToken)
        {
            var transaction = 
                await _unitOfWork.TransactionRepository
                .GetByFilterCriteriasAsync(cancellationToken, request.SenderNumber, request.RecipientNumber, request.TransactionNumber, request.StartDateTime, request.EndDateTime);

            return new GetTransactionsResponse { Transactions = _mapper.Map<List<TransactionEntity>, List<TransactionDTO>>(transaction) };
        }
    }
}
