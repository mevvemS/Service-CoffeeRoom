using Telegram.Bot.Types.ReplyMarkups;

namespace ServiceСoffeeRoom.Extentions
{
    public static class Keyboards
    {
        public static ReplyKeyboardMarkup ForCancel() => new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Отмена" } }) { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForRoomAdmin() => new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { $"Выйти из админки" } }) { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForCoffeemachineUse() => new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { $"Выйти из комнаты" } }) { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForUseCup() => new ReplyKeyboardMarkup(new[] {
            new KeyboardButton[] { $"Сделать кофе", $"Выйти из комнаты"  } })
        { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForUseService() => new ReplyKeyboardMarkup(new[] {
            new KeyboardButton[] { $"Выполнить сервис", $"Выйти из комнаты"  } })
        { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForCreateRoom() => new ReplyKeyboardMarkup(new[] { new[] { new KeyboardButton("Создать комнату"), new KeyboardButton("Отмена") } })
        { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForFullRightsRoom() => new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Воспользоваться", "Администрировать", "Отмена" } }) { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForUserRightsRoom() => new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Воспользоваться", "Отмена" } }) { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup ForAdminRightsRoom() => new ReplyKeyboardMarkup(new[] { new KeyboardButton[] { "Администрировать", "Отмена" } }) { ResizeKeyboard = true };
        public static ReplyKeyboardMarkup RequestUser()
            => new ReplyKeyboardMarkup(new[] { new[] {
                KeyboardButton.WithRequestUser("Выбрать контакт",new KeyboardButtonRequestUser(){UserIsBot =false }),
                new KeyboardButton("Выйти из админки") }})
            { ResizeKeyboard = true };

    }
}
