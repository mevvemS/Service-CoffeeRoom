using ServiceCoffeeRoom.Core.Abstraction;
using ServiceСoffeeRoom.Domain.Base;
using System.Linq.Expressions;

namespace ServiceСoffeeRoom.Infrastructure.Implementation
{
    public class InMemoryRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : struct
    {
        protected IEnumerable<TEntity> Data { get; set; }
        protected static object lockObject = new object();

        public InMemoryRepository(IEnumerable<TEntity> data)
        {
            Data = data;
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
        {
            return Task.FromResult(Data);
        }

        public Task<TEntity?> GetByIdAsync(TId id, CancellationToken token = default)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
        {
            try
            {

                token.ThrowIfCancellationRequested();
                Monitor.Enter(lockObject);
                IEnumerable<TEntity> enumetable = Data.Concat(new[] { entity });
                Data = enumetable;
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            return Task.FromResult(entity);
        }

        public Task<bool> UpdateAsync(TEntity entity, CancellationToken token = default)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                Monitor.Enter(lockObject);
                var date = Data.Where(x => !x.Id.Equals(entity.Id)).ToList();
                date.Add(entity);
                Data = date;
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(TEntity entity, CancellationToken token = default)
            => DeleteAsync(entity.Id, token);
        public Task<bool> DeleteAsync(TId id, CancellationToken token = default)
        {
            try
            {
                token.ThrowIfCancellationRequested();
                Monitor.Enter(lockObject);
                IEnumerable<TEntity> enumetable = Data.Where(x => x.Id.Equals(id));
                Data = enumetable;
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
            finally
            {
                Monitor.Exit(lockObject);
            }

            return Task.FromResult(true);
        }

        public Task<IEnumerable<TEntity>> GetByFilter(Expression<Func<TEntity, bool>> expression, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
