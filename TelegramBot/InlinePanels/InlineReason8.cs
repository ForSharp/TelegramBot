using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReason8 : InlineAbstractPanel
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
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reason8"), "Reason8.png")),
                    replyMarkup: inlineKeyBoard);
                
                var caption = await BotController.Bot.EditMessageCaptionAsync(userId, messageId,
                    "Для наших клиентов мы предоставляем каталоги и стенды, а также проводим обучение " +
                    "Вашего персонала работе с продукцией наших основных поставщиков.", 
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