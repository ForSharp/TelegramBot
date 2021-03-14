using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReason4 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });

                var message = await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reason4"), "Reason4.png")),
                    replyMarkup: inlineKeyBoard);
                
                var caption = await BotController.Bot.EditMessageCaptionAsync(userId, messageId,
                    "На нашем складе всегда в наличии более 15000 позиций товара, но если вдруг " +
                    "Вам нужна нестандартная позиция, мы привезем её под заказ.", 
                    replyMarkup: inlineKeyBoard);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}