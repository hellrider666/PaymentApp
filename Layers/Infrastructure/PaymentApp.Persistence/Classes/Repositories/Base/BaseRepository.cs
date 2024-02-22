using Microsoft.EntityFrameworkCore;
using PaymentApp.Application.Classes.Repositories.Base;
using PaymentApp.Domain.Entities.Base;
using PaymentApp.Persistence.Classes.Context;

namespace PaymentApp.Persistence.Classes.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepostiory<T> where T : BaseEntity
    {
        private readonly IAppDbContext _DbContext;

        public BaseRepository(IAppDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public void Create(T entity)
        {
            _DbContext.AddEntity(entity);
        }

        public void Delete(T entity)
        {
            _DbContext.GetDbSet<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _DbContext.GetDbSet<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> GetAsync(ulong Id, CancellationToken cancellationToken)
        {
            return await _DbContext.GetDbSet<T>().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public void Update(T entity)
        {
            _DbContext.GetDbSet<T>().Update(entity);
        }

        protected IQueryable<T> GetInternal()
        {
            return _DbContext.GetDbSet<T>();
        }
    }
}
