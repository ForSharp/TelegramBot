using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineContacts : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
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
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Contacts"), "Contacts.png")),
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, @"ООО ""Планета Групп""
Страна: Россия
Регион: Краснодарский край
Индекс: 350059

Адрес: 
г.Краснодар, ул. Уральская, 75к2, 
помещение 202,204

Телефоны:
+79298455400
+78612040013
+79284540013

E-mail: info@planeta-grupp.ru

Время работы:
пн-чт: 8:30-17:30
пт:    8:30-16:30
сб-вс: выходной", replyMarkup: inlineKeyBoard);

                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Contacts);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}