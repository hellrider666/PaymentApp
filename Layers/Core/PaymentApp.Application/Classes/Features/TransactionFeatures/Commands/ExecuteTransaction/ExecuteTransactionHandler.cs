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
            return await Execute(async unitOfWork =>
            {
                var sender = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(async rep =>
                {
                    return await rep.GetByAccountNumberAsync(request.SenderNumber, cancellationToken);
                });

                var recipient = await unitOfWork.DoWork<ICustomerRepository, CustomerEntity, CustomerEntity>(async rep =>
                {
                    return await rep.GetByAccountNumberAsync(request.RecipientNumber, cancellationToken);
                });

                unitOfWork.DoWork(() =>
                {
                    sender.Balance -= request.Sum;
                    recipient.Balance += request.Sum;
                });

                unitOfWork.DoWork<ITransactionRepository, TransactionEntity>(rep =>
                {
                    var transaction = new TransactionEntity
                    {
                        SenderCustomer = sender,
                        RecipientCustomer = recipient,
                        TransactionNumber = request.TransactionNumber,
                        Sum = request.Sum,
                        ExecutingDateTime = DateTime.Now
                    };

                    rep.Create(transaction);
                });

                return new ExecuteTransactionResponse { Successful = true };
            });           
        }
    }
}
