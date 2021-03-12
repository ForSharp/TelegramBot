using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineContacts : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });
            
                await BotController.Bot.EditMessageMediaAsync(
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Contacts"), "Contacts.png")),
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, @"ООО ""Планета Групп""
Страна: Россия
Регион: Краснодарский край
Индекс: 350059
Адрес: 
г.Краснодар, ул.Новороссийская, 238/1
Телефоны:
8(861)204-00-13
8(928)454-00-13
E-mail: info@planeta-grupp.ru
Время работы:
пн-чт: 8:30-17:30
пт:    8:30-16:30
сб-вс: выходной", replyMarkup: inlineKeyBoard);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}