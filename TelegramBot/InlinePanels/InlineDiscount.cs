using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineDiscount : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });
                
                await BotController.Bot.EditMessageMediaAsync(
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Discount"), "Discount.png")),
                    replyMarkup: inlineKeyBoard);
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, 
                    "В скором времени тут появятся акции", replyMarkup: inlineKeyBoard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}