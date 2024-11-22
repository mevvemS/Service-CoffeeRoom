using LinqToDB;
using LinqToDB.AspNet;
using ServiceCoffeeRoom.Repositories.DataAccess;
using ServiceСoffeeRoom;
using ServiceСoffeeRoom.Settings;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddEnvironmentVariables(prefix: "MYBOT_");
builder.Services.AddHostedService<Worker>();
var connectionSrting = builder.Configuration.Get<ServiceSettings>().DATA_TELEGRAM;
builder.Services.AddLinqToDBContext<ApiDataContext>((provider, options)
=> options
               .UsePostgreSQL(connectionSrting),
               lifetime: ServiceLifetime.Singleton);
builder.Services.AddTelegramBot(builder.Configuration)
                .AddRepositories()
                .AddServises()
                .AddClients();
var host = builder.Build();
host.Run();
