using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public abstract class InlineAbstractPanel
    {
        
        public void RunCreatingProcess(MessageEventArgs messageEventArgs, bool deleting = false)
        {
            if (deleting)
            {
                DeleteOldPanel(messageEventArgs);
                CreateInlinePanel(messageEventArgs);
            }
            else
            {
                EditInlinePanel(messageEventArgs, DataBaseContext.GetMessageId(messageEventArgs));
            }
        }

        protected void RunDefaultCreatingProcess(MessageEventArgs messageEventArgs)
        {
            DeleteOldPanel(messageEventArgs);
            var inlineMenu = new InlineMenu();
            inlineMenu.CreateInlinePanel(messageEventArgs);
        }

        protected virtual async void CreateInlinePanel(MessageEventArgs messageEventArgs)
        {
            var message = await BotController.Bot.SendPhotoAsync(messageEventArgs.Message.From.Id, "", replyMarkup: (InlineKeyboardMarkup) null);
            DataBaseContext.SaveMessageId(messageEventArgs, message.MessageId);
        }

        protected virtual async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            await BotController.Bot.EditMessageMediaAsync(messageEventArgs.Message.From.Id, messageId,
                new InputMediaPhoto(""), replyMarkup: (InlineKeyboardMarkup) null);
            await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, "",
                replyMarkup: (InlineKeyboardMarkup) null);
        }

        private static async void DeleteOldPanel(MessageEventArgs messageEventArgs)
        {
            try
            {
                await BotController.Bot.DeleteMessageAsync(messageEventArgs.Message.From.Id, DataBaseContext.GetMessageId(messageEventArgs));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}