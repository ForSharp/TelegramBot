using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.InlinePanels;

namespace TelegramBot
{
    public static class TextMessageProcessor
    {
        private static async void CreateKeyboardButtons(MessageEventArgs messageEventArgs)
        {
            if (DataBaseContext.GetCommandId(messageEventArgs.Message.From.Id) != (int) AdminCommandStep.Default)
                return;
            
            try
            {
                var replyKeyboard = CreateDefaultButtons();
                
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Здравствуйте, " +
                    $"{messageEventArgs.Message.From.FirstName}! \nПожалуйста, воспользуйтесь кнопками для начала работы", 
                    replyMarkup: replyKeyboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        public static ReplyKeyboardMarkup CreateDefaultButtons()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Меню"),
                    new KeyboardButton("Контакты")
                },
                new[]
                {
                    new KeyboardButton("Заказать звонок"),
                    new KeyboardButton("Показать на карте")
                }
            }, true, true);
            return replyKeyboard;
        }
        

        public static async void CreateStartStatement(MessageEventArgs messageEventArgs)
        {
            CreateKeyboardButtons(messageEventArgs);
            var inlineMenu = new InlineMenu();
            inlineMenu.RunCreatingProcess(messageEventArgs, true);
        }

        public static async void ShowInTheMap(MessageEventArgs messageEventArgs)
        {
            await BotController.Bot.SendLocationAsync(messageEventArgs.Message.From.Id, 45.0386671f, 39.066130f);
        }

        public static async void SendContacts(MessageEventArgs messageEventArgs)
        {
            await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, @"ООО ""Планета Групп""
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
сб-вс: выходной");
        }

        public static async void GetUserNumber(MessageEventArgs messageEventArgs)
        {
            await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, "GetUserNumber");
        }
        
    }
}