using System.ComponentModel;
using Telegram.Bot.Types.ReplyMarkups;

namespace ServiceСoffeeRoom.Extentions
{
    public static class InLineKeyboard
    {
        public static InlineKeyboardMarkup AdminRoom() => new(new[]
            {
                new[]{ InlineKeyboardButton.WithCallbackData("\t👋 Yчастник", "/roomAddUser"),InlineKeyboardButton.WithCallbackData("\t🥜 Зерна", "/roomAddBeans") },
                new[]{ InlineKeyboardButton.WithCallbackData("\t⏳ Интервал", "/roomEditServiceInterval"), InlineKeyboardButton.WithCallbackData("\t🛠️ Цена", "/roomEditServiceCoins") },
                new[]{ InlineKeyboardButton.WithCallbackData("\t🏷️ Изменить Имя", "/roomEditNameRoom") }                
            });
        public static InlineKeyboardMarkup UseCoffeeMachine(CoffeeMachineStatus status) => new(new[]
            {
                new[]{status switch
                {
                    CoffeeMachineStatus.Good => InlineKeyboardButton.WithCallbackData("\t☕ Кофе", "/userUse"),
                    CoffeeMachineStatus.NoBeans => InlineKeyboardButton.WithCallbackData("\t🥜 Зерна", "/userAddBeans") ,
                    CoffeeMachineStatus.ServiceRequired => InlineKeyboardButton.WithCallbackData("\t🛠️ Сервис", "/userAddService") 
                } , InlineKeyboardButton.WithCallbackData("\t💲 Баланс", "/userAddBalance")}
            });
    }
    [Description("Actions on the coffee machine")]
    public enum CoffeeMachineStatus 
    {
        [Description("A cup of coffee is available")]
        Good = 0,
        [Description("Add beans action")]
        NoBeans = 1,
        [Description("We need to conduct a service")]
        ServiceRequired = 2
    }
}
