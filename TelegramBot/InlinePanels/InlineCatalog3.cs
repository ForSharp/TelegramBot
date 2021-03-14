using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatalog3: InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Вихрь", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_vihr.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Вихрь ручной инструмент", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_vihr_ruchnoy.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Greenel", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_grinel.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Меркурий", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_merkuriy.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ресанта", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_resanta.pdf")
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
            
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Catalog3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}