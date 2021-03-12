using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineBrands : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Список брендов")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Инфо")
                    }
                });

                await BotController.Bot.EditMessageMediaAsync(
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media:  new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Brands"), "Brands.png")),
                    replyMarkup: inlineKeyBoard);
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, "Бренды", replyMarkup: inlineKeyBoard);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}