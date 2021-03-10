using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public abstract class InlinePanel
    {
        
        public void RunCreatingProcess(MessageEventArgs messageEventArgs)
        {
            DataBaseContext.DeleteOldPanel(messageEventArgs);
            CreateInlinePanel(messageEventArgs);
        }

        protected virtual async void CreateInlinePanel(MessageEventArgs messageEventArgs)
        {
            var message = await BotLogic.Bot.SendPhotoAsync(messageEventArgs.Message.From.Id, "", replyMarkup: (InlineKeyboardMarkup) null);
            DataBaseContext.SaveMessageId(messageEventArgs, message.MessageId);
        }

        
       
    }
}