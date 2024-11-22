using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain.Processes;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class CupRepository(ApiDataContext context) : ICupRepository
    {
        public async Task<CupOfCoffe> AddAsync(CupOfCoffe entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(CupOfCoffe entity, CancellationToken token = default)
            => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            await context.Cups.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<CupOfCoffe>> GetAllAsync(CancellationToken token = default)
        {
            return await context.Cups.ToListAsync(token: token);
        }

        public async Task<IEnumerable<CupOfCoffe>> GetByFilter(Expression<Func<CupOfCoffe, bool>> expression, CancellationToken token = default)
        {
            var query = context.Cups.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<CupOfCoffe?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await context.Cups.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(CupOfCoffe entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true;
        }
    }
}
