using ServiceСoffeeRoom.Domain.Base;
using LinqToDB.Mapping;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ServiceСoffeeRoom.Domain.Processes
{
    [Table("Payments")]
    public class Payment : Process
    {
        [Column]
        public int Price { get; private set; }
        public Payment(Room room, Person person, int count) :this (Guid.NewGuid(), room, person, count)
        {
        }

        public Payment(Guid id, Room room, Person person, int count) : base(id, room, person)
        {
            Price = count;
        }
        protected Payment() : base(Guid.NewGuid()) 
        {
        
        }
    }
}
