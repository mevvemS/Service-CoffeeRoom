using ServiceСoffeeRoom.Domain.Base;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public interface IRepository<TEntity,in TId> 
        where TEntity : Entity<TId>
        where TId: struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default);
        Task<TEntity?> GetByIdAsync(TId id, CancellationToken token = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken token = default);
        Task<bool> DeleteAsync(TEntity entity, CancellationToken token = default);
        Task<bool> DeleteAsync(TId id, CancellationToken token = default);
        Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> expression, CancellationToken token = default);
        
    }
}
