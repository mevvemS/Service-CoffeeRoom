using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceCoffeeRoom.Services.Applications.DtoModel.CoffeeMachineModel;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceCoffeeRoom.Services.Applications.Exeptions;
using ServiceСoffeeRoom.Applications.Abstractions;
using ServiceСoffeeRoom.Clients.Abstractions;
using ServiceСoffeeRoom.Extentions;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace ServiceСoffeeRoom.Clients
{
    public class CoffeMachineClient(ITelegramBotClient client,
        IRoomService roomService,
        IPersonService personService,
        ICupService cupService,
        IPaymentService paymentService,
        IServiceService serviceService) : MessageClient(client)
    {
        public async Task<long> StartCoffeeMachine(long chatId, PersonDto user, CancellationToken token)
        {
            var room = await roomService.GetRoom(token);
            //Логика контроллера.
            return await CreateMessage(chatId, room.CoffeeMachine, user, token);
        }

        public async Task<long> AddCash(long chatId, Message messageValue, PersonDto? user, CancellationToken token) 
        {
            int.TryParse(messageValue.Text, out int cash);
            var result = await paymentService.AddCashAsync(user!.Id, cash, token);
            if (result)
                user = await personService.GetPersonByIdAsync(user.Id,token);
            return result switch
            {
                true => await UpdateMessage(chatId, user!, token),
                false => chatId
            };

        }

        public async Task<long> AddServiceProcedureAsync(long chatId, PersonDto? user, CancellationToken token)
        {
            var result = await serviceService.AddServiceAsync(user!.Id, token);
            if (result)
                user = await personService.GetPersonByIdAsync(user.Id, token);
            return result switch
            {
                true => await UpdateMessage(chatId, user!, token),
                false => chatId
            };
        }

        public async Task<long> UseCoffeeMachine(long chatId, Message messageValue, PersonDto user, CancellationToken token)
        {
            if (await cupService.UseCoffeeMachineAsync(user.Id))
            { 
                user = await personService.GetPersonByIdAsync(user.Id, token)
                    ?? throw new EntiyNotFoundExeption(user.Id.ToString(), nameof(PersonDto));
                return await UpdateMessage(chatId, user, token);            
            }
            return chatId;
        }

        private async Task<long> CreateMessage(long chatId, CoffeeMachineDto? coffee, PersonDto user, CancellationToken token)
        {
            CoffeeMachineStatus status = GetStatus(coffee);

            var result = await client.SendPhotoAsync(
                chatId: chatId,
                photo: InputFile.FromStream(stream: new MemoryStream(File.ReadAllBytes(ImagesCatalog.GetCoffeeMachine()))),
                caption: TextMessages.CoffeemachineUserInfo(coffee, user),
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                replyMarkup: InLineKeyboard.UseCoffeeMachine(status),
                cancellationToken: token);
            return Add(result).Chat.Id;
        }

        private static CoffeeMachineStatus GetStatus(CoffeeMachineDto? coffee)
        {
            CoffeeMachineStatus status = CoffeeMachineStatus.Good;
            if (coffee?.Beans is null || coffee.Beans.Status is false)
                status = CoffeeMachineStatus.NoBeans;
            if (coffee?.CountCupService is 0)
                status = CoffeeMachineStatus.ServiceRequired;
            return status;
        }

        public async Task<long> UpdateMessage(long chatId, PersonDto user, CancellationToken token)
        {
            var message = await GetMessageById(chatId, token);
            if (message is null)
                return chatId;
            var room = await roomService.GetRoom(token);
            var coffee = room!.CoffeeMachine;
            CoffeeMachineStatus status = GetStatus(coffee);
            var result = await client.EditMessageCaptionAsync(chatId: chatId,
                            messageId: message.MessageId, 
                            caption: TextMessages.CoffeemachineUserInfo(coffee, user),
                            parseMode: Telegram.Bot.Types.Enums.ParseMode.Html,
                            replyMarkup: InLineKeyboard.UseCoffeeMachine(status),
                            cancellationToken: token);
            return result.Chat.Id;
        }

        public async Task<long> RemoveDialog(long key, CancellationToken token) => (await Remove(key, token));
    }
}
