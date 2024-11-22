using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public interface IRoomRepository: IRepository<Room, Guid>
    {
    }
}
