using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot8Lab
{
    class StartlessonCommand : Command
    {
        public override string Name => "lesson";

        public async override void Action(ITelegramBotClient client, Chat chat, int messageId)
        {
            string message;
            var factory = new ConnectionFactory() { HostName = "172.20.10.10", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "lesson_start", durable: true, exclusive: false, autoDelete: false);

                message = $"{chat.FirstName} {chat.LastName} почав лекцію";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: string.Empty, routingKey: "lesson_start", body: body);

                Console.WriteLine(" [x] Sent {0}", message);

            }
            await client.SendTextMessageAsync(chatId: chat, "We sent message about your lesson");
        }
    }
}
