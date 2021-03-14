using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineListBrands : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
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
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("ListBrand"), "ListBrand.png")),
                    replyMarkup: inlineKeyBoard);
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                    "Компания реализует продукцию торговых марок «Datarex», «Ecola», «Legrand», «LK60 VINTAGE», «Makel», " +
                    "«Uniel», «VOLPE», «Вихрь», «Greenel», «Меркурий», «Ресанта», «Хютер», «Brixoll», «Ambrella», «EKF», " +
                    "«Schneider Electric», «IEK».", replyMarkup: inlineKeyBoard);
                
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.ListBrands);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}