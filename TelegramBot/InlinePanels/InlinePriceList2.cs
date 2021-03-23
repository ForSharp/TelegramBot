using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlinePriceList2 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("МезонинЪ", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_mezonin_17062019s.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Кабель", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_kabel_0508201c.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Schneider Electric", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_schneider_16032020_0.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ecols", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_ecola_10042020.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Uniel", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_uniel_25032020.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало"),
                        InlineKeyboardButton.WithCallbackData("Далее")
                    }
                });

                await BotController.Bot.EditMessageMediaAsync(
                chatId: userId,
                messageId: messageId,
                media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("PriceList"), "PriceList.png")),
                replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, "Прайс-листы:", replyMarkup: inlineKeyBoard);

                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.PriceList2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}