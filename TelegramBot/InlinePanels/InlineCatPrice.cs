using System;
using System.Data.SQLite;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatPrice : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Каталоги продукции")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Прайс листы")
                    }
                });

                await BotController.Bot.EditMessageMediaAsync(
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("CatPrise"), "CatPrise.png")),
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, 
                    "Мы готовы предложить Вам широкий ассортимент кабельно-проводниковой и электротехнической продукции: все " +
                    "виды кабеля и провода, светильники и лампы, электроустановочные изделия, кабеленесущие системы, модульное " +
                    "электрооборудование, щиты, счетчики и многое другое. Компания ООО «Планета Групп» имеет прямые договора с производителями, " +
                    "что позволяет держать наши цены на самом низком уровне.", replyMarkup: inlineKeyBoard);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}