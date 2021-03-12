using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReason1 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
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
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reason1"), "Reason1.png")),
                    replyMarkup: inlineKeyBoard);
                
                var caption = await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId,
                    "Мы являемся официальными дистрибьюторами ведущих электротехнических компаний России и предоставляем своим клиентам лучшие цены.", 
                    replyMarkup: inlineKeyBoard);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}