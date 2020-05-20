using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotListener
{
    class Program
    {
        private static int trofimid = 354504594;
        private static int anisimovid = 264618297;
        static bool cansend = true;

        private static ITelegramBotClient _client;
        private static List<Command> _commands;

        static void Main(string[] args)
        {
            _client = new TelegramBotClient("1131920122:AAFjNH3kiOA6wUrCu-cfycdprDXOP9GxdFk");

            var me = _client.GetMeAsync().Result;
            Console.WriteLine("Bot name: " + me.FirstName);
            //Console.WriteLine("Token: " + args[0]);
            _commands = new List<Command>();

            while (true)
            {

                
                SetForTime("08:30:00");
                SetForTime("10:25:00");
                SetForTime("12:20:00");
                SetForTime("14:15:00");
               
                Thread.Sleep(86400000);
            }


        }

        public static void SetForTime(string DailyTime)
        {
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                       int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }


            Task.Delay(ts).ContinueWith((x) => WaitForMessage());
        }
        static public void WaitForMessage()
        {
            
            
            var factory = new ConnectionFactory() { HostName = "172.20.10.10", Port = 5672 };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: "lesson_start", durable: true, exclusive: false, autoDelete: false);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (eventModel, arg) =>
                {
                    var message = Encoding.UTF8.GetString(arg.Body.ToArray());
                    Console.WriteLine("Recieved message: " + message);
                    _client.SendTextMessageAsync(chatId: trofimid, message);
                    _client.SendTextMessageAsync(chatId: anisimovid, message);


                };

                for (int i = 0; i < 15; i++)
                {
                    channel.BasicConsume(queue: "lesson_start", autoAck: true, consumer: consumer);
                    Thread.Sleep(60000);
                }


            }
          
            
            
        }



    }
}
