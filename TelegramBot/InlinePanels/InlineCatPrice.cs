using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatPrice : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Каталоги продукции"),
                        InlineKeyboardButton.WithCallbackData("Прайс листы")
                    },
                    new[]
                    {
                        
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });

                await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("CatPrice"), "CatPrice.png")),
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                    "Мы готовы предложить Вам широкий ассортимент кабельно-проводниковой и электротехнической продукции: все " +
                    "виды кабеля и провода, светильники и лампы, электроустановочные изделия, кабеленесущие системы, модульное " +
                    "электрооборудование, щиты, счетчики и многое другое. Компания ООО «Планета Групп» имеет прямые договора с производителями, " +
                    "что позволяет держать наши цены на самом низком уровне.", replyMarkup: inlineKeyBoard);
                
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.CatPrice);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}