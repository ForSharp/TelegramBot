using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReason5 : InlineAbstractPanel
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
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reason5"), "Reason5.png")),
                    replyMarkup: inlineKeyBoard);
                
                var caption = await BotController.Bot.EditMessageCaptionAsync(userId, messageId,
                    "Мы обеспечиваем молниеносную доставку заказных позиций электротоваров.", 
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