using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlinePriceList1 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                        {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("LK60, Vintage", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_lk_60_vintage_15022018s.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Makel", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_makel_310119.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Меркурий счетчики", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_merkuriy_22022019_0.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Светодиодные строки", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_svetodiodnye_stroki_2709_0.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Greenel", "http://planeta-grupp.ru/sites/default/files/filesdoc/prays_grinel_17062019s.xls")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало"),
                        InlineKeyboardButton.WithCallbackData("Далее")
                    }
                    });

                await BotController.Bot.EditMessageMediaAsync(
                chatId: messageEventArgs.Message.From.Id,
                messageId: messageId,
                media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("PriceList"), "PriceList.png")),
                replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, "Прайс-листы:", replyMarkup: inlineKeyBoard);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}