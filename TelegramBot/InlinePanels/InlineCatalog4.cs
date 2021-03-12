using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineCatalog4: InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
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
                chatId: messageEventArgs.Message.From.Id,
                messageId: messageId,
                media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Catalog"), "Catalog.png")),
                replyMarkup: inlineKeyBoard);
            
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, 
                    "К вашему выбору каталоги нашей продукции:", replyMarkup: inlineKeyBoard);
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}