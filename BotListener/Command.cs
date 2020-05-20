using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BotListener
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Action(ITelegramBotClient client, Chat chat, int messageId);

        public bool CheckCommand(string command)
        {
            return command.Contains(this.Name);
        }
    }
}
