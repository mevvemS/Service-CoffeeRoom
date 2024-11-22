using ServiceCoffeeRoom.Services.Applications.DtoModel.Room;

namespace ServiceСoffeeRoom.Applications.Abstractions
{
    public interface IRoomService
    {
        Task<RoomDto?> GetRoom(CancellationToken token = default);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto roomInfo, CancellationToken token = default);
        Task<bool> DeleteRoom(Guid id,CancellationToken token = default);
        Task<bool> UpdateRoom(UpdateRoomDto roomInfo, CancellationToken token = default);
    }
}
