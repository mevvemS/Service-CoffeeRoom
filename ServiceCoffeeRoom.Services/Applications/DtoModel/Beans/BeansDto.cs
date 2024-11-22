using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCoffeeRoom.Services.Applications.DtoModel.Beans
{
    public class BeansDto
    {
        public Guid Id { get; set; }
        public required string Mark { get;  set; }
        public int Weight { get; set; }
        public int Price { get; set; }
        public int RemainingWeight { get; set; }
        public bool Status { get; set; }
    }
}
