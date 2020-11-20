using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionDataUploader.Core.Infrastructure.Persistence
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> AddAsync(TEntity entity);
        Task<List<TEntity>> AddRangeAsync(List<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
