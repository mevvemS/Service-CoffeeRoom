using ServiceСoffeeRoom.Domain.Processes;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public interface IServiceRepository : IRepository<Service,Guid>
    {
    }
}
