using ServiceСoffeeRoom.Domain.Base;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ServiceСoffeeRoom.Domain.Processes
{
    [Table("Services")]
    public class Service : Process
    {
        [Column]
        public int Price { get; private set; }
        public Service(Room room, Person person, int price) : this(Guid.NewGuid(), room, person, price)
        {
        }
        public Service(Guid id, Room room, Person person, int price) : base(id, room, person)
        {
            Price = price;
            room.AddCashBank(-1 * price);
            person.AddCash(price);

        }
        protected Service() : base(Guid.NewGuid()) 
        {
        
        }
    }
}
