using ServiceСoffeeRoom.Extentions;
using Telegram.Bot.Types;

namespace ServiceСoffeeRoom.Clients
{
    public class CallbackClient(DialogueClient dialogueClient, RoomClient roomClient)
    {
        public async Task<Message?> ProcessingCallback(Update update, CancellationToken token = default)
        {            
            var command = update.CallbackQuery!.Data;
            var message = update.CallbackQuery!.Message;
            Message? result = command switch
            {
                "/roomAddUser" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/roomAddUser", TextMessages.RequestUser(), Keyboards.RequestUser, token),               
                "/roomAddBeans" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/roomAddBeans", TextMessages.RequestAddBeans(), Keyboards.ForRoomAdmin, token),
                "/roomEditServiceCoins" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/roomEditServiceCoins", TextMessages.RequestServiceCoins(), Keyboards.ForRoomAdmin, token),
                "/roomEditServiceInterval" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/roomEditServiceInterval", TextMessages.RequestServiceInterval(), Keyboards.ForRoomAdmin, token),
                "/roomEditNameRoom" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/roomEditNameRoom", TextMessages.RequestEditName(), Keyboards.ForRoomAdmin, token),
                //"/roomDeleteUser" =>,
                //"/roomEditAdmin" =>,
                "/userUse" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/userUse", TextMessages.RequestUseCup(), Keyboards.ForUseCup, token),
                "/userAddBalance" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/userAddBalance", TextMessages.RequestAddUserCoins(), Keyboards.ForCoffeemachineUse, token),
                "/userAddService" => await dialogueClient.SendReplyKeyboardExpectedResponse(message.Chat.Id, "/userAddService", TextMessages.RequestUseService(), Keyboards.ForUseService, token),
                _ => message
            };
            return result;
        }



    }
}
