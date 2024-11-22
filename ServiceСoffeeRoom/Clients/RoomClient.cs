using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Beans;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Room;
using ServiceСoffeeRoom.Applications.Abstractions;
using ServiceСoffeeRoom.Clients.Abstractions;
using ServiceСoffeeRoom.Extentions;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace ServiceСoffeeRoom.Clients
{
    /// <summary>
    /// Содержит коллекции диологовых окон CRUD комнаты.
    /// </summary>
    /// <param name="client"></param>
    public class RoomClient(ITelegramBotClient client, 
        IRoomService roomService, 
        IPersonService personService,
        IBeansService beansService) : MessageClient(client)
    {
        public async Task<long> UpdateMessage(long chatId, CancellationToken token)
        {
            var message = await GetMessageById(chatId, token);
            if (message is null)
                return chatId;
            RoomDto? room = await roomService.GetRoom(token);
            var result = await client.EditMessageCaptionAsync(chatId: chatId,
                            messageId: message.MessageId,
                            caption: TextMessages.RoomAdminInfo(room),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                            replyMarkup: InLineKeyboard.AdminRoom(),
                            cancellationToken: token);
            return result.Chat.Id;
        }
        public async Task<long> AddBeansInRoom(long chatId, Message messageValue, CancellationToken token)
        {
            var room = await roomService.GetRoom(token);
            if (room is null)
                return chatId;

            var values = messageValue.Text!.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            int.TryParse(values[0], out int prive);
            int weight = 0;
            if(values.Length > 1) 
                int.TryParse(values[1], out weight);
            var beansInfo = new CreateBeansDto()
            {
                Price = prive,
                Mark = "CoffeeBeans",
                Weight = weight
            };
            _ = await beansService.AddBeansAsync(beansInfo, token);
            return chatId;
        }
        public async Task<long> AddUserInRoom(long chatId, long UserId, CancellationToken token)
        {
            _ = await personService.AddUserAsync(UserId, token);
            return await this.UpdateMessage(chatId, token);
        }
        public async Task<long> EditServiceIntervalInRoom(long chatId, Message messageValue, CancellationToken token)
        {
            var room = await roomService.GetRoom(token);
            if (room is null)
                return chatId;
            int.TryParse(messageValue.Text, out int value);
            if (value > 0 && value < 500)
            {
                var roomInfo = new UpdateRoomDto()
                {
                    Id = room.Id,
                    AdminId = room.Admin.Id,
                    Name = room.Name,
                    LimitService = value,
                    PriceService = room.PriceService
                };
                _ = await roomService.UpdateRoom(roomInfo, token);
                return await this.UpdateMessage(chatId, token);
            }

            return chatId;
        }
        public async Task<long> EditServiceCoinsInRoom(long chatId, Message messageValue, CancellationToken token)
        {
            var room = await roomService.GetRoom(token);
            if (room is null)
                return chatId;
            int.TryParse(messageValue.Text, out int value);
            if (value > 0 && value <= 5000)
            {
                var roomInfo = new UpdateRoomDto()
                {
                    Id = room.Id,
                    AdminId = room.Admin.Id,
                    Name = room.Name,
                    LimitService = room.CoffeeMachine!.LimitService,
                    PriceService = value
                };
                _ = await roomService.UpdateRoom(roomInfo, token);
                return await this.UpdateMessage(chatId, token);
            }

            return chatId;
        }
        public async Task<long> EditServiceNameInRoom(long chatId, Message messageValue, CancellationToken token)
        {
            var room = await roomService.GetRoom(token);
            if (room is null)
                return chatId;
            var roomInfo = new UpdateRoomDto()
            {
                Id = room.Id,
                AdminId = room.Admin.Id,
                Name = messageValue.Text!,
                LimitService = room.CoffeeMachine!.LimitService,
                PriceService = room.PriceService
            };
            _ = await roomService.UpdateRoom(roomInfo, token);
            return await this.UpdateMessage(chatId, token);
        }

        public async Task<long> StartRoom(long chatId, PersonDto admin, CancellationToken token)
        {
            var room = await roomService.GetRoom(token);
            if (room is null)
            {
                var roomInfo = new CreateRoomDto() { Name = "Уютная кофейня", AdminId = admin.Id };
                room = await roomService.CreateRoomAsync(roomInfo, token);
            }
            return await CreateMessage(chatId, room, token);
        }

        private async Task<long> CreateMessage(long chatId, RoomDto? room, CancellationToken token)
        {
            var result = await client.SendPhotoAsync(
                chatId: chatId,
                photo: InputFile.FromStream(stream: new MemoryStream(File.ReadAllBytes(ImagesCatalog.GetRoom()))),
                caption: TextMessages.RoomAdminInfo(room),
                parseMode:Telegram.Bot.Types.Enums.ParseMode.Html,
                replyMarkup: InLineKeyboard.AdminRoom(),
                cancellationToken: token);
            return Add(result).Chat.Id;
        }

        public async Task<long> RemoveRoomDialog(long key, CancellationToken token) => await Remove(key, token);

    }
}
