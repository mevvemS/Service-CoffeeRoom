using ServiceCoffeeRoom.Services.Applications.DtoModel.Beans;

namespace ServiceCoffeeRoom.Services.Applications.DtoModel.CoffeeMachineModel
{
    public class CoffeeMachineDto
    {
        public string Name { get; set; }
        public BeansDto? Beans { get; set; }
        public int PriceСup { get; set; }
        public int CountCupAll { get; set; }
        public int CountCupService { get; set; }
        public int LimitService { get; set; }
        public int SizeOfOneCup { get; set; }
    }
}
