using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Bot8Lab
{
    //Trofim id: 354504594

    //Anisimov id: 264618297

    class Program
    {
        private static int trofimid = 354504594;
        private static int anisimovid = 264618297;

        private static ITelegramBotClient _client;
        private static List<Command> _commands;

        static void Main(string[] args)
        {
            _client = new TelegramBotClient("1277866234:AAGC5RHjVsy7-UNZfVzmwmPi7lrTgz_CBF4");
            
            var me = _client.GetMeAsync().Result;
            Console.WriteLine("Bot name: " + me.FirstName);
            //Console.WriteLine("Token: " + args[0]);
            _commands = new List<Command>();

            _commands.Add(new StartCommand());
            _commands.Add(new StartlessonCommand());

            //{
            //    new StartCommand(),
            //    new KillCommand(),
            //    new ZerotwoCommand()

            //};
            
            _client.OnMessage += _client_OnMessage;
            _client.StartReceiving();




            Thread.Sleep(Timeout.Infinite);
        }

        static bool cansend=true;
        private async static void _client_OnMessage(object sender, MessageEventArgs e)
        {
            
            var text = e?.Message?.Text;
            Console.WriteLine(e.Message.Chat.FirstName + " " + e.Message.Chat.LastName + " " + text);
            if (text == null)
                return;
            var chatId = e.Message.Chat;
            var messageId = e.Message.MessageId;
            foreach (Command command in _commands)
            {
                if (command.CheckCommand(text))
                {
                    command.Action(_client, chatId, messageId);

                    return;
                }

            }
            await _client.SendTextMessageAsync(chatId, "I don`t know what do you want to do");
        }
    }
}
