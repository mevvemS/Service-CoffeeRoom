using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain.Processes;
using Expressions = System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class ServiceRepository(ApiDataContext context) : IServiceRepository
    {
        public async Task<Service> AddAsync(Service entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(Service entity, CancellationToken token = default)
            => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            await context.Services.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<Service>> GetAllAsync(CancellationToken token = default)
        {
            var result = await context.Services.ToListAsync(token: token);
            return result;
        }

        public async Task<IEnumerable<Service>> GetByFilter(Expressions.Expression<Func<Service, bool>> expression, CancellationToken token = default)
        {
            var query = context.Services.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<Service?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await context.Services.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(Service entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true; ;
        }
    }
}
