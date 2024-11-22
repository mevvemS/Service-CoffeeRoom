using ServiceCoffeeRoom.Services.Applications.DtoModel.CoffeeMachineModel;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Person;
using ServiceCoffeeRoom.Services.Applications.DtoModel.Room;

namespace ServiceСoffeeRoom.Extentions
{
    public static class TextMessages
    {
        public static string GetHolloyAndGoToAdmin(string nameRoom, string telegramAccaunt)
            => $"<i>Приветствуем в нашей уютной кофейной комнатe:</i> <b>{nameRoom}!</b>\n" +
               $"<blockquote>Чтобы пользоваться кофемашиной\n" +
               $"Сделайте запрос администратору: @{telegramAccaunt}</blockquote>";
        public static string CreateRoom()
            => $"<blockquote>У меня еще нет <b>кофейной комнаты.</b>\n" +
               $"Создадим ее?</blockquote>";

        public static string Greeting(string personName)
             => $"<b><i>{personName}</i></b>, добро пожаловать в <b>Кофейную комнату</b>!\n";
        public static string FullRights()
         => $"<blockquote>Вы можете воспользоваться кофемашиной и администрировать ее.\n" +
            $"Что будете делать?</blockquote>";
        public static string AdminRights(string personName)
         => $"<b><i>{personName}</i></b>, привет!\n" +
            $"<blockquote>Вы можете администрировать кофемашину.</blockquote>";
        public static string UserRights(string personName)
         => $"<b><i>{personName}</i></b>, привет!\n" +
            $"<blockquote>Вы можете воспользоваться кофемашиной.</blockquote>";

        public static string RoomAdminInfo(RoomDto roomDto)
            => "Добро пожаловать в:\n" +
                $"\t🏷️<b><i>{roomDto.Name}</i></b>\n" +                
                "<blockquote expandable><i><u>Кабинет администратора.</u></i>\n" +
                $"Выпито \t☕: <b>{roomDto.CoffeeMachine?.CountCupAll}</b>\n" +
                $"\t💲Баланс: <b>{roomDto.Bank}</b>\n" +
                "<i><u>Установлены параметры:</u></i>\n" +
                $"\t👤Количество пользователей: <b>{roomDto.Users.Count}</b>\n" +
                $"\t⏳Интервал сервиса: <b>{roomDto.CoffeeMachine?.LimitService}</b> чашек\n" +
                $"\t🛠️Цена сервиса: <b>{roomDto.PriceService}</b>\n" +
                $"До сервиса осталось: <b>{roomDto.CoffeeMachine?.CountCupService}</b> чашек</blockquote>";
        public static string CoffeemachineUserInfo(CoffeeMachineDto? coffeeMachine, PersonDto user)
            => "<i><u>Кофемашина:</u></i>\n" +
                $"\t🏷️<b><i>{coffeeMachine?.Name}</i></b>\n" +
                "<blockquote expandable><u>Текущие характеристики</u>\n" +
                $"\t🥜Статус зерен: <b>{coffeeMachine?.Beans?.Status.BoolConvert()}</b>\n" +
                $"\t🛠️Чашек до сервиса: <b>{coffeeMachine?.CountCupService}</b>\n" +
                $"\t☕\t💲Стоимость чашки: <b>{coffeeMachine?.PriceСup}</b>\n" +
                $"\t💲Ваш текущий баланс: <tg-spoiler><b>{user.CashAccount.BillContertView()}</b></tg-spoiler></blockquote>";
        public static string Instruction(string name)
            => $"<i>Для настройки <b>{name}</b></i>\n" +
                "<blockquote> Bоспользуйтесь карточкой сверху.</blockquote>\n" +
                $"<i>Для выхода из <b>{name}</b></i>\n" +
                "<blockquote>воспользуйтесь командами снизу.</blockquote>";
        public static string Use()
       => $"<blockquote>Для <b>использования</b> воспользуйтесь <u><i>карточкой сверху</i></u>.\n" +
           "Для <b>выхода</b> воспользуйтесь <i><u>командами снизу</u></i>.</blockquote>";
        public static string RequestUser()
            => $"\t👤Выберите <i>контакт</i>, <u>командами снизу.</u>";
        public static string RequestServiceInterval()
            => $"\t🛠️Введите сервисный интервал кофемашины.\n" +
            $"<blockquote>Сервисный интервал измеряется в кол-ве\t☕.</blockquote>";
        public static string RequestServiceCoins()
            => $"\t💲\t🛠️Введите стоимасть сервиса кофемашины.\r\n" +
            $"<blockquote>Стоимость вводится в <i>условных единицах</i>\n" +
            $"<b>Целым числом.</b></blockquote>";
        public static string RequestAddUserCoins()
            => $"\t💲Введите размер пополения.\r\n" +
               $"<blockquote>Стоимость вводится в <i>условных единицах</i>\n" +
               $"<b>Целым числом.</b></blockquote>";
        public static string RequestEditName()
            => $"\t🏷️Введите новое имя <u>комнаты.</u>";
        public static string RequestUseCup()
            => $"\t☕<b>Подтвердите</b> получение <u>чашки кофя.</u>";
        public static string RequestUseService()
            => $"\t🛠️<b>Подтвердите</b> выполнение <u>сервиса.</u>";
        public static string RequestAddBeans()
            => $"\t🥜Введите параметры <i>зерен</i> в <u>формате:</u>\n" +
            $"<u><b>Цена</b></u>(целое число - руб.) <u><b>Вес</b></u><i>*</i>(целое число - гр.)\n" +
            $"<i>*-если вес 1000г. его можно не указывать.</i>\n" +
            $"<blockquote><b>1525</b>\n" +
            $"<b>625</b> <b>350</b></blockquote>";
    }
    public static class Converter 
    {
        public static string BoolConvert(this bool value) => value switch { true => "OK", false => "STOP" };
        public static string BillContertView(this int value) => value switch {
            <= -100 => value.ToString(),
            <= -10 => $"-0{value*-1}",
            < 0 => $"-00{value*-1}",
            < 10 => $"000{value}",
            < 100 => $"00{value}",
            < 1000 => $"0{value}", 
            >= 1000 => value.ToString() };
    }
}
