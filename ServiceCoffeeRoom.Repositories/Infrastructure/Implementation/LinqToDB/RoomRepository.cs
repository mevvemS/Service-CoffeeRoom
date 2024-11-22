using LinqToDB;
using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom.Domain;
using System.Linq.Expressions;

namespace ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB
{
    public class RoomRepository(ApiDataContext context) : IRoomRepository
    {
        public async Task<Room> AddAsync(Room entity, CancellationToken token = default)
        {
            await context.InsertWithIdentityAsync(entity, token: token);
            return entity;
        }

        public async Task<bool> DeleteAsync(Room entity, CancellationToken token = default)
         => await DeleteAsync(entity.Id, token);

        public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
        {
            await context.Rooms.DeleteAsync(s => s.Id == id, token);
            return true;
        }

        public async Task<IEnumerable<Room>> GetAllAsync(CancellationToken token = default)
        {
            return await context.Rooms.ToListAsync(token: token);
        }

        public async Task<IEnumerable<Room>> GetByFilter(Expression<Func<Room, bool>> expression, CancellationToken token = default)
        {
            var query = context.Rooms.Where(expression);
            return await query.ToListAsync(token: token);
        }

        public async Task<Room?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await context.Rooms.FirstOrDefaultAsync(s => s.Id == id, token);
        }

        public async Task<bool> UpdateAsync(Room entity, CancellationToken token = default)
        {
            await context.UpdateAsync(entity, token: token);
            return true;
        }
    }
}
