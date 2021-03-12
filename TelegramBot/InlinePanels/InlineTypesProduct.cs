using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineTypesProduct : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            //TODO
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Перейти к каталогу")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });
                await BotController.Bot.EditMessageMediaAsync(
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("ListBrand"), "ListBrand.png")),
                    replyMarkup: inlineKeyBoard);
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, 
                    @"Кабельно-проводниковая и электротехническая продукция от производителя.

Все виды кабеля и провода, светильники и лампы, электроустановочные изделия, кабеленесущие системы, модульное электрооборудование, щиты, счетчики и другое", 
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