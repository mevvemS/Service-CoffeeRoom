namespace ServiceCoffeeRoom.Services.Applications.DtoModel.Room
{
    public class UpdateRoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long AdminId { get; set; }
        public int PriceService { get; set; }
        public int LimitService { get; set; }
    }
}
