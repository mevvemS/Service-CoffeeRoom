using ServiceСoffeeRoom.Domain.Processes;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public interface ICupRepository: IRepository<CupOfCoffe, Guid>
    {
    }
}
