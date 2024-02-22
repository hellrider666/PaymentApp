using PaymentApp.Domain.Entities.Base;

namespace PaymentApp.Domain.Entities
{
    public class CustomerEntity : BaseEntity
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public List<TransactionEntity> ReceivedTransactions { get; set; }
        public List<TransactionEntity> SentTransactions { get; set; }
    }
}
