using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReason7 : InlineAbstractPanel
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
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reason7"), "Reason7.png")),
                    replyMarkup: inlineKeyBoard);
                
                var caption = await BotController.Bot.EditMessageCaptionAsync(userId, messageId,
                    "Вся продукция, реализуемая компанией, имеет соответствующие сертификаты и гарантии, " +
                    "в случае обнаружения дефектов товара, мы готовы произвести замену или возврат в кратчайшие сроки.", 
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