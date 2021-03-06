using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlinePriceList3 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ambrella", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_ambrella_23032020.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("IEK", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_iek_23032020.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Legrand", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_legrand_23032020.xlsx_.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("EKF", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_ekf_23032020.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Brixoll", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_brixoll_25032020.xls")
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
                media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("PriceList"), "PriceList.png")),
                replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, "Прайс-листы:", replyMarkup: inlineKeyBoard);

                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.PriceList3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}