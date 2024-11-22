using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceСoffeeRoom.Applications.DtoModel.Person
{
    public class UpdatePersonDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TelegramAccaunt { get; set; }
    }
}
