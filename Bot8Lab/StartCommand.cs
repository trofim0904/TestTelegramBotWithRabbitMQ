using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Bot8Lab
{
    public class StartCommand : Command
    {
        public override string Name => "start";

        public async override void Action(ITelegramBotClient client, Chat chat, int messageId)
        {
            //Console.WriteLine("id: " + chat.Id);
            await client.SendTextMessageAsync(chatId: chat, "pruvet " + chat.FirstName);
            
        }
    }
}
