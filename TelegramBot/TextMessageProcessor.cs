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
            try
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
                
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Здравствуйте, " +
                    $"{messageEventArgs.Message.From.FirstName}! \nПожалуйста, воспользуйтесь кнопками для начала работы", 
                    replyMarkup: replyKeyboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        public static void CreateStartStatement(MessageEventArgs messageEventArgs)
        {
            CreateKeyboardButtons(messageEventArgs);
            var inlineMenu = new InlineMenu();
            inlineMenu.RunCreatingProcess(messageEventArgs, true);
        }

        public static async void ShowInTheMap(MessageEventArgs messageEventArgs)
        {
            await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, "ShowInTheMap");
        }

        public static async void SendContacts(MessageEventArgs messageEventArgs)
        {
            await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, "SendContacts");
        }

        public static async void GetUserNumber(MessageEventArgs messageEventArgs)
        {
            await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, "GetUserNumber");
        }
        
    }
}