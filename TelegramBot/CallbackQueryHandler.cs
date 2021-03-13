using System;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class CallbackQueryHandler
    {
        public static void HandleCallbackQuery(MessageEventArgs messageEventArgs)
        {
            var test = DataBaseContext.GetMessageId(messageEventArgs);
            
            if (test == (int) InlinePanelSteps.Brands)
            {
                
            }
        }
    }
}