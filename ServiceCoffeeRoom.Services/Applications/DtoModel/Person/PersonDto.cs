using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCoffeeRoom.Services.Applications.DtoModel.Person
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TelegramAccaunt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsUser { get; set; }
        public int CashAccount { get; set; }
    }
}
