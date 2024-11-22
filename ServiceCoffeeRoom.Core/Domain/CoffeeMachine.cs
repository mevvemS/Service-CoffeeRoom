using ServiceCoffeeRoom.Core.Domain.Base;
using ServiceСoffeeRoom.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ServiceСoffeeRoom.Domain
{
    [Table("CoffeeMachines")]
    public class CoffeeMachine : Entity<Guid>, IProtorype<CoffeeMachine>
    {
        const int _sizeOfOneCup = 18;
        const int _limitService = 50;

        [Column]
        public string Name { get; private set; }

        [Column]
        [ForeignKey("FK_CoffeeMachines_BeansBags_BeansId")]
        public Guid BeansId { get; set; }
        public Beans? Beans { get; }
        [Column]
        public int CountCupAll { get; set; }

        [Column]
        public int CountCupService { get; set; }

        [Column]
        public int LimitService { get; set; }

        [Column]
        public int SizeOfOneCup { get; set; }
        public CoffeeMachine(Guid id, string name) : base(id)
        {
            Name = name;
            CountCupAll = 0;
            SizeOfOneCup = _sizeOfOneCup;
            SetProperty(_limitService);
        }
        public CoffeeMachine(string name)
            : this(Guid.NewGuid(), name)
        {

        }
        protected CoffeeMachine() : base(Guid.NewGuid())
        {

        }
        public void SetName(string name) => Name = name;
        public void SetProperty(int limitService)
        {
            LimitService = limitService;
            CountCupService = LimitService;
        }
        public void SetBeans(Beans beans) => BeansId = beans.Id;
        public bool UseCup() => CountCupService switch
        {
            > 0 => AddCup(),
            _ => false,
        };
        public bool AddService() => CountCupService switch
        {
            0 => CountService(),
            _ => false,
        };
        bool CountService()
        {
            CountCupService = LimitService;
            return true;
        }

        bool AddCup()
        {
            CountCupAll++;
            CountCupService--;
            return true;
        }
        internal double GetIndexCup(Beans? beans)
        {
            if (beans is null)
                return 0;
            if (beans.Id.Equals(BeansId))
            {
                var countCup = (double)beans.Weight / SizeOfOneCup;
                return beans.Price / countCup;
            }
            throw new Exception("Ошибка сервиса.");
        }

        public CoffeeMachine Prototype()
            => new CoffeeMachine(Id, Name)
            {
                BeansId = this.BeansId,
                CountCupAll = this.CountCupAll,
                CountCupService = this.CountCupService,
                LimitService = this.LimitService,
                SizeOfOneCup = this.SizeOfOneCup
            };
    }
}
