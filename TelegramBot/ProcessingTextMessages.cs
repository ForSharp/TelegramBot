using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class ProcessingTextMessages
    {
        
        
        public static async void CreateKeyboardButtons(MessageEventArgs messageEventArgs)
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
                
                await BotLogic.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Здравствуйте, " +
                    $"{messageEventArgs.Message.From.FirstName}! \nПожалуйста, воспользуйтесь кнопками для начала работы", 
                    replyMarkup: replyKeyboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }
        
        
    }
}