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
            var userId = messageEventArgs.Message.From.Id;
            if (deleting)
            {
                DeleteOldPanel(userId);
                CreateInlinePanel(userId);
            }
            else
            {
                EditInlinePanel(userId, DataBaseContext.GetMessageId(userId));
            }
        }

        public void RunCreatingProcess(CallbackQueryEventArgs callbackQueryEventArgs)
        {
            var userId = callbackQueryEventArgs.CallbackQuery.From.Id;
            EditInlinePanel(userId, DataBaseContext.GetMessageId(userId));
        }

        protected void RunDefaultCreatingProcess(int userId)
        {
            DeleteOldPanel(userId);
            var inlineMenu = new InlineMenu();
            inlineMenu.CreateInlinePanel(userId);
        }


        protected virtual async void CreateInlinePanel(int userId)
        {
            var message = await BotController.Bot.SendPhotoAsync(userId, "", replyMarkup: (InlineKeyboardMarkup) null);
            DataBaseContext.SaveMessageId(userId, message.MessageId);
        }

        protected virtual async void EditInlinePanel(int userId, int messageId)
        {
            await BotController.Bot.EditMessageMediaAsync(userId, messageId,
                new InputMediaPhoto(""), replyMarkup: (InlineKeyboardMarkup) null);
            await BotController.Bot.EditMessageCaptionAsync(userId, messageId, "",
                replyMarkup: (InlineKeyboardMarkup) null);
        }

        private static async void DeleteOldPanel(int userId)
        {
            try
            {
                await BotController.Bot.DeleteMessageAsync(userId, DataBaseContext.GetMessageId(userId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}