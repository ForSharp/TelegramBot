using System;
using System.Linq;
using System.Threading;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class AdminCommand
    {
        private static readonly string[] KeyWords = {"Подтвердить", "Отмена", "Назад"};
        public static void HandleDefaultCommands(int userId, string message)
        {
            if (message == "/admin")
            {
                GetAdminCommands(userId);
            }
                    
            if (message == "/appoint")
            {
                ShowUsers(userId);
            }

            if (message == "/message")
            {
                GetForwardingMessage(userId);
            }
                    
            if (message == "/timetable")
            {
                TimetableEditor.StartEditTimetable(userId);
            }
        }

        public static async void HandleTargetName(int userId, string message)
        {
            var tempUserName = message;
            DataBaseContextAdmin.SetTargetName(userId, tempUserName);
                    
            if (message == "Подтвердить")
            {
                await BotController.Bot.SendTextMessageAsync(userId, "Введите username");
            }
            if (message == "Назад")
            {
                Undo(userId);
            }
            if (message == "Отмена")
            {
                Undo(userId);
            }
            if (!KeyWords.Contains(message))
            {
                ConfirmUser(userId, tempUserName);
            }
        }

        public static void HandleConfirmingUser(MessageEventArgs messageEventArgs, int userId, string message)
        {
            if (message == "Подтвердить")
            {
                AppointAdmin(messageEventArgs, DataBaseContextAdmin.GetTargetName(userId));
                        
                Thread.Sleep(10);
                Undo(userId);
            }
            if (message == "Назад")
            {
                ShowUsers(userId);
            }
            if (message == "Отмена")
            {
                Undo(userId);
            }
        }

        public static async void HandleSendMessage(MessageEventArgs messageEventArgs, int userId, string message)
        {
            var forwardingMessage = messageEventArgs.Message;
            DataBaseContextAdmin.SetForwardingMessageId(userId, forwardingMessage.MessageId);
            if (message == "Подтвердить")
            {
                await BotController.Bot.SendTextMessageAsync(userId, "Введите сообщение для рассылки.");
            }
            if (message == "Назад")
            {
                Undo(userId);
            }
            if (message == "Отмена")
            {
                Undo(userId);
            }
            if (!KeyWords.Contains(message))
            {
                ConfirmForwardingMessage(userId, forwardingMessage.MessageId);
            }
        }

        public static void HandleConfirmingSending(int userId, string message)
        {
            if (message == "Подтвердить")
            {
                var usersId = DataBaseContextAdmin.GetAllUserId();
                foreach (var targetId in usersId)
                {
                    ForwardMessage(targetId, userId, DataBaseContextAdmin.GetForwardingMessageId(userId));
                }
                        
                Thread.Sleep(10);
                Undo(userId);
            }
            if (message == "Назад")
            {
                GetForwardingMessage(userId);
            }
            if (message == "Отмена")
            {
                Undo(userId);
            }
        }
        
        private static async void ShowUsers(int userId)
        {
            try
            {
                var users = DataBaseContextAdmin.GetAllUserNames();
                var userNames = string.Join(", ", users);
                await BotController.Bot.SendTextMessageAsync(userId,
                    "Выберите и введите username пользователя, которого хотите назначить администратором.", 
                    replyMarkup: KeyboardContainer.CreateThreeKeyboardAdminButtons());
                Thread.Sleep(10);
                await BotController.Bot.SendTextMessageAsync(userId, userNames);

                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ShowUsers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async void ConfirmUser(int userId, string userName)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId,
                    $"Вы собираетесь присвоить права администратора пользователю {userName}?");
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ConfirmUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        private static async void AppointAdmin(MessageEventArgs messageEventArgs, string userName)
        {
            try
            {
                DataBaseContextAdmin.ChangeUserStatus(userName);
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, 
                    $"Пользователю {userName} присвоены права администратора.");
                await BotController.Bot.SendTextMessageAsync(DataBaseContextAdmin.GetUserIdByUserName(userName), 
                    $"{BotController.GetAvailableSenderName(messageEventArgs)} " +
                    $"присвоил вам права администратора. Для вывода команд введите /admin");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await BotController.Bot.SendTextMessageAsync(messageEventArgs.Message.From.Id, 
                    $"Пользователю {userName} не удалось присвоить права администратора. \n{e.Message}");
            }
        }

        private static async void GetForwardingMessage(int userId)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId,
                    "Отправьте сообщение для рассылки (только текст (обычный и форматированный) и эмодзи).", 
                    replyMarkup:  KeyboardContainer.CreateThreeKeyboardAdminButtons());
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SendMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static async void ConfirmForwardingMessage(int userId, int messageId)
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

        private static async void ForwardMessage(int targetId, int userId, int messageId)
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

        private static async void GetAdminCommands(int userId)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId,
                    $"Команды администратора:" +
                    $"\n/appoint - назначить нового администратора" +
                    $"\n/message - отправить всем пользователям сообщение" +
                    $"\n/timetable - редактировать расписанией рейсов");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private static async void Undo(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, "Для вывода команд введите /admin",
                replyMarkup: KeyboardContainer.CreateDefaultKeyboard());
            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
        }
    }
}