using AutoMapper;
using PaymentApp.Application.Classes.Mapping.Interfaces;
using PaymentApp.Domain.Entities;

namespace PaymentApp.Application.Classes.DTOs
{
    public class CustomerDTO : IMapWith<CustomerEntity>
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerEntity, CustomerDTO>();
        }
    }
}
