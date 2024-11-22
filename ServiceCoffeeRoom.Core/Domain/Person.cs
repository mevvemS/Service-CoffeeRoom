using ServiceCoffeeRoom.Core.Domain.Base;
using ServiceСoffeeRoom.Domain.Base;
using ColumnAttribute = LinqToDB.Mapping.ColumnAttribute;
using TableAttribute = LinqToDB.Mapping.TableAttribute;

namespace ServiceСoffeeRoom.Domain
{
    [Table("Persons")]
    public class Person : Entity<long>, IProtorype<Person>
    {
        [Column]
        public string? Name { get; private set; }

        [Column]
        public string? TelegramAccaunt { get; private set; }

        [Column]
        public bool IsAdmin { get; private set; }

        [Column]
        public bool IsUser { get; private set; }

        [Column]
        public int CashAccount { get; set; }

        public Person(long id, string? name, string? telegramAccaunt, bool isAdmin, bool isUser) : base(id)
        {
            Id = id;
            Name = name;
            TelegramAccaunt = telegramAccaunt;
            IsAdmin = isAdmin;
            IsUser = isUser;
            CashAccount = 0;
        }
        public Person(long id)
        : this(id, null, null, false, true)
        {

        }
        protected Person() : base(0) 
        {            
        }
        public void SetName(string name) => Name = name;
        public void SetTelegramAccaunt(string telegramAccaunt) => TelegramAccaunt = telegramAccaunt;
        public void SetIsAdmin(bool isAdmin) => IsAdmin = isAdmin;
        public void SetIsUser(bool isUser) => IsUser = isUser;
        public void AddCash(int account)
        {
            if (IsUser)
                CashAccount += account;
            else throw new Exception("Нет доступа.");
        }

        public Person Prototype()
            => new Person(Id, Name, TelegramAccaunt, IsAdmin, IsUser) 
            { 
                CashAccount = this.CashAccount 
            };
    }
}
