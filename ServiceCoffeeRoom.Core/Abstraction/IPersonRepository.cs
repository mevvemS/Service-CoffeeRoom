using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Core.Abstraction
{
    public interface IPersonRepository: IRepository<Person, long> 
    {
    }
}
