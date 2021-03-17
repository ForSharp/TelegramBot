using System;
using System.Collections.Generic;
using System.Threading;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class AdminCommand
    {
        public static async void ShowUsers(int userId)
        {
            var users = DataBaseContext.GetAllUserNames();
            var userNames = string.Join(", ", users);
            await BotController.Bot.SendTextMessageAsync(userId,
                "Выберите и введите username пользователя, которого хотите назначить администратором.");
            Thread.Sleep(10);
            await BotController.Bot.SendTextMessageAsync(userId, userNames);

            CreateKeyboardButtons(userId);
            
            DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.ShowUsers);
        }

        public static async void ConfirmUser(int userId, string userName)
        {
            await BotController.Bot.SendTextMessageAsync(userId,
                $"Вы собираетесь присвоить права администратора пользователю {userName}?");
            
            DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.ConfirmUser);
        }
        
        
        private static async void CreateKeyboardButtons(int userId)
        {
            try
            {
                var replyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Подтвердить")
                    },
                    new[]
                    {
                        new KeyboardButton("Назад"),
                        new KeyboardButton("Отмена")
                    }
                }, true, true);
                
                await BotController.Bot.SendTextMessageAsync(userId, ".", 
                    replyMarkup: replyKeyboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        public static async void AppointAdmin(MessageEventArgs messageEventArgs, string userName)
        {
            try
            {
                DataBaseContext.ChangeUserStatus(userName);
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Пользователю {userName} присвоены права администратора.");
                await BotController.Bot.SendTextMessageAsync(DataBaseContext.GetUserIdByUserName(userName), $"{BotController.GetAvailableSenderName(messageEventArgs)}" +
                    $"присвоил вам права администратора. Для вывода команд введите /admin");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Пользователю {userName} не удалось присвоить права администратора. \n{e.Message}");
            }
           
        }
    }
}