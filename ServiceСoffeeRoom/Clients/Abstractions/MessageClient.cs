using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ServiceСoffeeRoom.Clients.Abstractions
{
    public abstract class MessageClient(ITelegramBotClient client)
    {
        readonly ConcurrentDictionary<long, Message> date = new();
        protected async Task<Message>  GetMessageById(long id, CancellationToken token) 
        {
            token.ThrowIfCancellationRequested();
            date.TryGetValue(id, out Message? oldMessage);
            return oldMessage;
        }
        protected Message Add(Message message)
        {
            date.TryAdd(message.Chat.Id, message);
            return message;
        }

        protected async Task<long> Remove(long key, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            date.TryRemove(key, out Message? message);
            if (message is not null)
                await client.DeleteMessageAsync(chatId: key, messageId: message.MessageId, token);
            return key;
        }

        protected async Task<Message> Update(Message message, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            date.TryGetValue(message.Chat.Id, out Message? oldMessage);
            if (oldMessage is not null)
            {
                await client.DeleteMessageAsync(chatId: message.Chat.Id, messageId: oldMessage.MessageId, token);
                date.TryUpdate(message.Chat.Id, message, oldMessage);
            }
            else Add(message);
            return message;
        }
    }
}
