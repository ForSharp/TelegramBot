using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatalog4: InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Хютер", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_hyuter.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Brixoll", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_brixoll_obshchiy.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Brixoll Control", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_brixoll_control.pdf")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Ambrella", "http://planeta-grupp.ru/sites/default/files/filesdoc/katalog_ambrella.pdf")
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
                media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Catalog"), "Catalog.png")),
                replyMarkup: inlineKeyBoard);
            
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                    "К вашему выбору каталоги нашей продукции:", replyMarkup: inlineKeyBoard);
            
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Catalog4);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}