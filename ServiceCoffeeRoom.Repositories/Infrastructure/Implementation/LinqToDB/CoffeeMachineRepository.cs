using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class CoffeeMachineRepository(ApiDataContext context)  : ICoffeeMachineRepository
    {
        public async Task<CoffeeMachine> AddAsync(CoffeeMachine entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(CoffeeMachine entity, CancellationToken token = default)
            => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            await context.CoffeeMachines.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<CoffeeMachine>> GetAllAsync(CancellationToken token = default)
        {
            return await context.CoffeeMachines.ToListAsync(token: token);
        }

        public async Task<IEnumerable<CoffeeMachine>> GetByFilter(Expression<Func<CoffeeMachine, bool>> expression, CancellationToken token = default)
        {
            var query = context.CoffeeMachines.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<CoffeeMachine?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await context.CoffeeMachines.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(CoffeeMachine entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true;
        }
    }
}
