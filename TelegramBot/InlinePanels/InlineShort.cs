using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineShort : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                        {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Подробнее о компании")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Каталоги и прайсы")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });
            
                await BotController.Bot.EditMessageMediaAsync(
            chatId: userId,
            messageId: messageId,
            media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Short"), "Short.png")),
            replyMarkup: inlineKeyBoard);
            
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, @"Электротехнические материалы и оборудование оптом в Краснодаре.

Компания ""Планета Групп"" – это стабильно развивающаяся оптовая компания, с 2008 года торгующая кабельно-проводниковой, электротехнической и светотехнической продукцией на рынке Краснодарского края и ЮФО. Мы обеспечиваем комплексные поставки электротехнических материалов и оборудования в торговые компании, объекты строительства и ведущие предприятия Краснодарского края.

Мы готовы предложить Вам широкий ассортимент кабельно - проводниковой и электротехнической продукции: все виды кабеля и провода, светильники и лампы, электроустановочные изделия, кабеленесущие системы, модульное электрооборудование, щиты, счетчики и многое другое.Компания ООО «Планета Групп» имеет прямые договора с производителями, что позволяет держать наши цены на самом низком уровне.", replyMarkup: inlineKeyBoard);
            
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Short);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}