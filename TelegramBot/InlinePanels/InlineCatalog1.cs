using System;
using System.Data.SQLite;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatalog1 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                        {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Datarex", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_datarex.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ecola", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_ecola.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ecola GX53, GX70", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_ecola_gx53_gx70.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ecola MR16", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_ecola_mr16.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Legrand", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_legrand.pdf")
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
                media:  new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Catalog"), "Catalog.png")),
                replyMarkup: inlineKeyBoard);
            
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                    "К вашему выбору каталоги нашей продукции:", replyMarkup: inlineKeyBoard);
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
    
}