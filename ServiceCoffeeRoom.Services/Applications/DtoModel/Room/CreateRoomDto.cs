using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCoffeeRoom.Services.Applications.DtoModel.Room
{
    public class CreateRoomDto
    {
        public string Name { get; set; }
        public long AdminId { get; set; }
    }
}
