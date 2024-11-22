using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public interface ICoffeeMachineRepository : IRepository<CoffeeMachine, Guid>
    {
    }
}
