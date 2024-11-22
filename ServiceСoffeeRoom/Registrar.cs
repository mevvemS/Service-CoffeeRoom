using ServiceCoffeeRoom.Core.Abstraction;
using ServiceCoffeeRoom.Repositories.Infrastructure.Implementation.LinqToDB;
using ServiceCoffeeRoom.Services.Applications;
using ServiceCoffeeRoom.Services.Applications.Abstractions;
using ServiceСoffeeRoom.Applications;
using ServiceСoffeeRoom.Applications.Abstractions;
using ServiceСoffeeRoom.Clients;
using ServiceСoffeeRoom.Settings;
using Telegram.Bot;

namespace ServiceСoffeeRoom
{
    public static class Registrar
    {
        public static IServiceCollection AddTelegramBot(this IServiceCollection services, IConfiguration configuration)
        {
            var keyTelegram = configuration.Get<ServiceSettings>().KEY_TELEGRAM;
            services.AddSingleton<ITelegramBotClient>(х =>  new TelegramBotClient(keyTelegram));
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPaymentRepository, PaymentRepository>();
            services.AddSingleton<ICupRepository, CupRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();
            services.AddSingleton<ICoffeeMachineRepository, CoffeeMachineRepository>();
            services.AddSingleton<IBeansRepository, BeansRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IServiceRepository, ServiceRepository>();
            return services;
        }
        public static IServiceCollection AddServises(this IServiceCollection services)
        {
            services.AddSingleton<IRoomService, RoomService>();
            services.AddSingleton<IPersonService, PersonService>();
            services.AddSingleton<IBeansService, BeansService>();
            services.AddSingleton<ICupService, CupService>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IServiceService, ServiceService>();
            return services;
        }
        public static IServiceCollection AddClients(this IServiceCollection services)
        {
            services.AddSingleton<DialogueClient>();
            services.AddSingleton<CallbackClient>();
            services.AddSingleton<RoomClient>();
            services.AddSingleton<CoffeMachineClient>();
            return services;
        }

    }
}
