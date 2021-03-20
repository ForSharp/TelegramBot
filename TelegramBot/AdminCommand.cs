using System;
using System.Threading;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class AdminCommand
    {
        public static async void ShowUsers(int userId)
        {
            var users = DataBaseContextAdmin.GetAllUserNames();
            var userNames = string.Join(", ", users);
            await BotController.Bot.SendTextMessageAsync(userId,
                "Выберите и введите username пользователя, которого хотите назначить администратором.", replyMarkup: CreateKeyboardButtons());
            Thread.Sleep(10);
            await BotController.Bot.SendTextMessageAsync(userId, userNames);

            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ShowUsers);
        }

        public static async void ConfirmUser(int userId, string userName)
        {
            await BotController.Bot.SendTextMessageAsync(userId,
                $"Вы собираетесь присвоить права администратора пользователю {userName}?");
            
            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ConfirmUser);
        }
        
        
        private static ReplyKeyboardMarkup CreateKeyboardButtons()
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

            return replyKeyboard;
        }

        public static async void AppointAdmin(MessageEventArgs messageEventArgs, string userName)
        {
            try
            {
                DataBaseContextAdmin.ChangeUserStatus(userName);
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Пользователю {userName} присвоены права администратора.");
                await BotController.Bot.SendTextMessageAsync(DataBaseContextAdmin.GetUserIdByUserName(userName), $"{BotController.GetAvailableSenderName(messageEventArgs)} " +
                    $"присвоил вам права администратора. Для вывода команд введите /admin");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, $"Пользователю {userName} не удалось присвоить права администратора. \n{e.Message}");
            }
        }

        public static async void GetForwardingMessage(int userId)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId,
                    "Отправьте сообщение для рассылки (только текст (обычный и форматированный) и эмодзи).", replyMarkup: CreateKeyboardButtons());
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SendMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async void ConfirmForwardingMessage(int userId, int messageId)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId,
                    "Отправить всем следующее сообщение?");
                await BotController.Bot.ForwardMessageAsync(userId, userId, messageId);
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ConfirmSending);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public static async void ForwardMessage(int targetId, int userId, int messageId)
        {
            try
            {
                await BotController.Bot.ForwardMessageAsync(targetId, userId, messageId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async void EditTimetable(int userId)
        {
            try
            {
                var replyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    new[]
                    {
                        new KeyboardButton("Добавить рейс"),
                        new KeyboardButton("Редактировать рейс"),
                        new KeyboardButton("Удалить рейс")
                    },
                    new[]
                    {
                        new KeyboardButton("Отмена")
                    }
                }, true, true);
            
                await BotController.Bot.SendTextMessageAsync(userId,
                    "Выберите действие с расписанием", replyMarkup: replyKeyboard);
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.EditTimetable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}