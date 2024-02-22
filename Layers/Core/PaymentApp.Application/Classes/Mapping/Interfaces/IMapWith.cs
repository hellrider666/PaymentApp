using AutoMapper;

namespace PaymentApp.Application.Classes.Mapping.Interfaces
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
