using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class PersonRepository(ApiDataContext context) : IPersonRepository
    {
        public async Task<Person> AddAsync(Person entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(Person entity, CancellationToken token = default)
            => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(long id, CancellationToken token = default)
        {
            await context.Persons.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken token = default)
        {
            return await context.Persons.ToListAsync(token: token);
        }

        public async Task<IEnumerable<Person>> GetByFilter(Expression<Func<Person, bool>> expression, CancellationToken token = default)
        {
            var query = context.Persons.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<Person?> GetByIdAsync(long id, CancellationToken token = default)
        {
            return await context.Persons.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(Person entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true;
        }
    }
}
