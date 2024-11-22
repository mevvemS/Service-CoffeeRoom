using ServiceСoffeeRoom.Domain.Base;
using LinqToDB.Mapping;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ServiceСoffeeRoom.Domain.Processes
{
    [Table("Cups")]
    public class CupOfCoffe : Process
    {
        [Column]
        public int Price { get; private set; }

        public CupOfCoffe(Guid id, Room room, int price, Person person) : base(Guid.NewGuid(), room, person)
        {
            Price = price;
            person.AddCash(-1 * Price);
            room.AddCashBank(Price);
        }
        public CupOfCoffe(Room room, int price , Person person) : this(Guid.NewGuid(), room, price, person)
        {           
        }
        protected CupOfCoffe() : base(Guid.NewGuid())
        {
        }
    }
}
