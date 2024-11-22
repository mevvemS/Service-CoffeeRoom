using ServiceСoffeeRoom.Domain.Base;
using LinqToDB.Mapping;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;
using ServiceCoffeeRoom.Core.Domain.Base;

namespace ServiceСoffeeRoom.Domain
{
    [Table("BeansBags")]
    public class Beans : Entity<Guid>, IProtorype<Beans>
    {
        const int _defaultWeight = 1000;

        [Column]
        public string Mark { get; private set; }

        [Column]
        public int Weight { get; private set; }

        [Column]
        public int Price { get; private set; }

        [Column]
        public int RemainingWeight { get; set; }

        [Column]
        public bool Status { get; set; }

        [Column]
        public DateTime TimeHappened { get; set; }
        public Beans(Guid id, string mark, int price, int weight = _defaultWeight) : base(id)
        {
            Mark = mark;
            Price = price;
            Weight = weight;
            RemainingWeight = Weight;
            Status = true;
            TimeHappened = DateTime.Now.ToUniversalTime();
        }

        public Beans(string mark, int price, int weight = _defaultWeight)
            :this(Guid.NewGuid(), mark, price, weight)
        {

        }
        protected Beans() : base(Guid.NewGuid()) 
        {
        
        }
        public bool Use(int weigthCup) 
        {
            if (Status)
                RemainingWeight -= weigthCup;

            if (RemainingWeight > weigthCup)
                Status = true;
            else Status = false;

            return Status;
        }

        public Beans Prototype()
        => new Beans(Id,Mark,Price,Weight) 
        {
            RemainingWeight = this.RemainingWeight,
            Status = this.Status
        };
    }
}
