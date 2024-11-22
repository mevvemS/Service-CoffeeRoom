using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain.Processes;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class PaymentRepository(ApiDataContext context) : IPaymentRepository
    {
        public async Task<Payment> AddAsync(Payment entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(Payment entity, CancellationToken token = default)
           => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            await context.Payments.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync(CancellationToken token = default)
        {
            return await context.Payments.ToListAsync(token: token);
        }

        public async Task<IEnumerable<Payment>> GetByFilter(Expression<Func<Payment, bool>> expression, CancellationToken token = default)
        {
            var query = context.Payments.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await context.Payments.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(Payment entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true;
        }
    }
}
