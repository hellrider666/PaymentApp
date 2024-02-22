using PaymentApp.Domain.Entities.Base;

namespace PaymentApp.Application.Classes.Repositories.Base
{
    public interface IBaseRepostiory<T> where T: BaseEntity
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetAsync(UInt64 Id, CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
