using ServiceCoffeeRoom.Core.Domain.Base;
using ServiceСoffeeRoom.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ServiceСoffeeRoom.Domain
{
    [Table("Rooms")]
    public class Room : Entity<Guid>, IProtorype<Room>
    {
        const int _priceService = 100;
        [Column]
        public string Name { get; private set; }

        [Column]
        [ForeignKey("FK_Rooms_Persons_AdminId")]
        public long AdminId { get; private set; }
        public Person? Admin { get; private set; }
        [Column]
        [ForeignKey("FK_Rooms_CoffeeMachines_CoffeeMachineId")]
        public Guid CoffeeMachineId { get;  set; }
        public CoffeeMachine? CoffeeMachine { get;  set; }

        [Column]
        public int PriceService { get; set; }

        [Column]
        public int Bank { get; set; }

        public Room(Guid id, long adminId, string nameRoom) : base(id)
        {
            Name = nameRoom;
            AdminId = adminId;
            SetPriceService(_priceService);
        }
        public Room(long adminId, string nameRoom)
            : this(Guid.NewGuid(), adminId, nameRoom)
        {

        }
        protected Room() : base(Guid.NewGuid())
        {

        }
        public void SetName(string name) => Name = name;
        public void SetCoffemachine(Guid coffeeMachineId) =>
            CoffeeMachineId = coffeeMachineId;
        public void SetPriceService(int service) => PriceService = service;
        public int GetCurrentPriceCoffeeCup(CoffeeMachine coffeeMachine, Beans beans)
        {
            var indexTray = GetIndex(PriceService, coffeeMachine.LimitService);
            var indexCup = coffeeMachine.GetIndexCup(beans);
            return (int)(indexCup + indexTray);

            double GetIndex(int prise, int limit) => (double)prise / limit;
        }
        public void AddCashBank(int costCup)
        {
            Bank += costCup;
        }

        public Room Prototype()
        => new Room(Id, AdminId, Name)
        {
            CoffeeMachineId = this.CoffeeMachineId,
            PriceService = this.PriceService,
            Bank = this.Bank
        };
    }
}
