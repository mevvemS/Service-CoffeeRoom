using LinqToDB;
using LinqToDB.Data;
using ServiceСoffeeRoom.Domain;
using ServiceСoffeeRoom.Domain.Processes;

namespace ServiceCoffeeRoom.Repositories.DataAccess
{
    public class ApiDataContext : DataConnection
    {
        public ApiDataContext(DataOptions<ApiDataContext> options)
        : base(options.Options) { }

        public ITable<Person> Persons => this.GetTable<Person>();
        public ITable<Room> Rooms => this.GetTable<Room>();
        public ITable<CoffeeMachine> CoffeeMachines => this.GetTable<CoffeeMachine>();
        public ITable<Beans> Beans => this.GetTable<Beans>();
        public ITable<CupOfCoffe> Cups => this.GetTable<CupOfCoffe>();
        public ITable<Service> Services => this.GetTable<Service>();
        public ITable<Payment> Payments => this.GetTable<Payment>();

    }
}
