using ServiceСoffeeRoom.Clients;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ServiceСoffeeRoom
{
    public class Worker(ILogger<Worker> logger, 
        ITelegramBotClient telegramBotClient, 
        DialogueClient dialogueClient,
        CallbackClient callbackClient) : BackgroundService
    {     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            telegramBotClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync,
                    new ReceiverOptions
                    {
                        AllowedUpdates = Array.Empty<UpdateType>(),
                    },
                    stoppingToken);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
            }
        }

        async Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            logger.LogError(exception.Message);
        }

        async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {           
            try
            {
                Message? result = update.Type switch
                {
                    UpdateType.Message => await dialogueClient.ProcessingMessage(update.Message, token),
                    UpdateType.CallbackQuery => await callbackClient.ProcessingCallback(update, token),
                    _ => update.Message
                };                

                logger.LogInformation(result?.Text);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
