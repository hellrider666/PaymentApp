using AutoMapper;
using MediatR;
using PaymentApp.Application.Classes.Abstract;
using PaymentApp.Application.Classes.Repositories;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.Features.TransactionFeatures.Commands.ExecuteTransaction
{
    public class ExecuteTransactionHandler : BaseHandler, IRequestHandler<ExecuteTransactionRequest, ExecuteTransactionResponse>
    {
        public ExecuteTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<ExecuteTransactionResponse> Handle(ExecuteTransactionRequest request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransaction(async () =>
            {
                var sender = await _unitOfWork.CustomerRepository.GetByAccountNumberAsync(request.SenderNumber, cancellationToken);

                var Recipient = await _unitOfWork.CustomerRepository.GetByAccountNumberAsync(request.RecipientNumber, cancellationToken);

                sender.Balance -= request.Sum;
                Recipient.Balance += request.Sum;

                var transaction = new TransactionEntity
                {
                    SenderCustomer = sender,
                    RecipientCustomer = Recipient,
                    TransactionNumber = request.TransactionNumber,
                    Sum = request.Sum,
                    ExecutingDateTime = DateTime.Now
                };

                _unitOfWork.TransactionRepository.Create(transaction);

                return new ExecuteTransactionResponse { Successful = true };
            });
        }
    }
}
