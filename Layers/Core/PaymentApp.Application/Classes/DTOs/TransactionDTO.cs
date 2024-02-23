using AutoMapper;
using PaymentApp.Application.Classes.Mapping.Interfaces;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.DTOs
{
    public class TransactionDTO : IMapWith<TransactionEntity>
    {
        public CustomerDTO SenderCustomer { get; set; }
        public CustomerDTO RecipientCustomer { get; set; }
        public string TransactionNumber { get; set; }
        public DateTime ExecutingDateTime { get; set; }
        public decimal Sum { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TransactionEntity, TransactionDTO>();
        }
    }


}
