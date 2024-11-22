using ServiceCoffeeRoom.Services.Applications.DtoModel.Beans;
using ServiceCoffeeRoom.Services.Applications.DtoModel.CoffeeMachineModel;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceСoffeeRoom.Domain;

namespace ServiceCoffeeRoom.Services.Applications.DtoModel.Room
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required AdminDto Admin { get; set; }
        public CoffeeMachineDto? CoffeeMachine { get; set; }
        public BeansDto CurrentBeans { get; set; }
        public int PriceService { get; set; }
        public int Bank { get; set; }
        public required ICollection<PersonDto> Users { get; set; }
    }
}
