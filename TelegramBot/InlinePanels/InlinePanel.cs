using System;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public abstract class InlinePanel
    {
        public void RunCreatingProcess(MessageEventArgs messageEventArgs)
        {
            //try
            {
                
                DataBaseContext.DeleteOldPanel(messageEventArgs);
                //DataBaseContext.DeleteAction(messageEventArgs);
                
            }
            //catch (Exception e)
            {
                //Console.WriteLine(e);
            }
            CreateInlinePanel(messageEventArgs, out var messageId);
            DataBaseContext.SaveMessageId(messageEventArgs, messageId);
        }

        protected virtual void CreateInlinePanel(MessageEventArgs messageEventArgs, out int messageId)
        {
            InlineKeyboardMarkup inlineKeyBoard = null;
            messageId = SendInlinePanel(messageEventArgs, inlineKeyBoard).Id;
        }

        protected abstract Task SendInlinePanel(MessageEventArgs messageEventArgs, InlineKeyboardMarkup inlineKeyboardMarkup);
        
        
    }
}