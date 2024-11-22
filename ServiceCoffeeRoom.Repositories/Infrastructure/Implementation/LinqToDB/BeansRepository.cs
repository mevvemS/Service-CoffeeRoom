using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class BeansRepository(ApiDataContext context) : IBeansRepository
    {
        public async Task<Beans> AddAsync(Beans entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(Beans entity, CancellationToken token = default)
           => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            await context.Beans.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<Beans>> GetAllAsync(CancellationToken token = default)
        {
            return await context.Beans.ToListAsync(token: token);
        }

        public async Task<IEnumerable<Beans>> GetByFilter(Expression<Func<Beans, bool>> expression, CancellationToken token = default)
        {
            var query = context.Beans.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<Beans?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await context.Beans.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(Beans entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true;
        }
    }
}
