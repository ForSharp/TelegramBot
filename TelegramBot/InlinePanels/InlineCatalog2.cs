using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatalog2: InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Legrand ЭУИ Valena LIFE&ALLURE", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_legrand_valena_lifeallure.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("LK60 VINTAGE", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_lk_studio_lk60_vintage.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Makel", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_makel.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Uniel", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_uniel.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("VOLPE (Uniel)", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_volpeuniel.pdf")
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
                media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Catalog"), "Catalog.png")),
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