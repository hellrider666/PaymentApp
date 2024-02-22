using PaymentApp.Domain.Entities.Base;
using PaymentApp.Domain.Entities.Enums;

namespace PaymentApp.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public CustomerEntity SenderCustomer { get; set; }
        public CustomerEntity RecipientCustomer { get; set; }
        public string TransactionNumber { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime ExecutingDateTime { get; set; }
        public decimal Sum { get; set; }
    }
}
