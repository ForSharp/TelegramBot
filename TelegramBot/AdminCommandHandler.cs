using System;
using System.Linq;
using System.Threading;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class AdminCommandHandler
    {
        public static async void HandleAdminCommands(MessageEventArgs messageEventArgs)
        {
            if (!DataBaseContextAdmin.CheckAdminRights(messageEventArgs.Message.From.Id))
                return;

            var userId = messageEventArgs.Message.From.Id;
            var message = messageEventArgs.Message.Text;
            
            var commandId = DataBaseContextAdmin.GetCommandId(userId);

            var keyWords = new[] {"Подтвердить", "Отмена", "Назад"};
            
            switch (commandId)
            {
                case (int) AdminCommandStep.Default:
                    if (message == "/a")
                    {
                        AdminCommand.ShowUsers(userId);
                    }

                    if (message == "/s")
                    {
                        AdminCommand.GetForwardingMessage(userId);
                    }
                    
                    if (message == "/t")
                    {
                        TimetableEditor.EditTimetable(userId);
                    }
                    break;
                case (int) AdminCommandStep.ShowUsers:
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
                    if (!keyWords.Contains(message))
                    {
                        AdminCommand.ConfirmUser(userId, tempUserName);
                    }
                    break;
                case (int) AdminCommandStep.ConfirmUser:
                    
                    if (message == "Подтвердить")
                    {
                        AdminCommand.AppointAdmin(messageEventArgs, DataBaseContextAdmin.GetTargetName(userId));
                        
                        Thread.Sleep(10);
                        Undo(userId);
                    }
                    if (message == "Назад")
                    {
                        AdminCommand.ShowUsers(userId);
                    }
                    if (message == "Отмена")
                    {
                        Undo(userId);
                    }
                    break;
                case (int) AdminCommandStep.SendMessage:
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
                    if (!keyWords.Contains(message))
                    {
                        AdminCommand.ConfirmForwardingMessage(userId, forwardingMessage.MessageId);
                    }
                    break;
                case (int) AdminCommandStep.ConfirmSending:
                    if (message == "Подтвердить")
                    {
                        var usersId = DataBaseContextAdmin.GetAllUserId();
                        foreach (var targetId in usersId)
                        {
                            AdminCommand.ForwardMessage(targetId, userId, DataBaseContextAdmin.GetForwardingMessageId(userId));
                        }
                        
                        Thread.Sleep(10);
                        Undo(userId);
                    }
                    if (message == "Назад")
                    {
                        AdminCommand.GetForwardingMessage(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        Undo(userId);
                    }
                    break;
                case (int) AdminCommandStep.EditTimetable:
                    if (message == "Добавить рейс")
                    {
                        DataBaseContextAdmin.AddTrip();
                        DataBaseContextAdmin.SetTripId(userId, DataBaseContextAdmin.GetLastTripId());
                        DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DeparturePlace);
                        await BotController.Bot.SendTextMessageAsync(userId, "Откуда рейс?", 
                            replyMarkup: KeyboardContainer.CreateTwoKeyboardAdminButtons());
                    }
                    if (message == "Редактировать рейс")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите ID рейса для редактирования.", 
                            replyMarkup: KeyboardContainer.CreateTwoKeyboardAdminButtons());
                        DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SetTripColumnEdit);
                    }
                    if (message == "Удалить рейс")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите ID рейса для удаления.",
                            replyMarkup: KeyboardContainer.CreateTwoKeyboardAdminButtons());
                        DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SetTripColumnDel);
                    }
                    if (message == "Отмена")
                    {
                        Undo(userId);
                    }
                    break;
                case (int) AdminCommandStep.DeparturePlace:
                    TimetableEditor.SetDeparturePlace(userId, message);
                    break;
                case (int) AdminCommandStep.DepartureDate:
                    TimetableEditor.SetDepartureDate(userId, message);
                    break;
                case (int) AdminCommandStep.DepartureTime:
                    TimetableEditor.SetDepartureTime(userId, message);
                    break;
                case (int) AdminCommandStep.ArrivalPlace:
                    TimetableEditor.SetArrivalPlace(userId, message);
                    break;
                case (int) AdminCommandStep.ArrivalDate:
                    TimetableEditor.SetArrivalDate(userId, message);
                    break;
                case (int) AdminCommandStep.ArrivalTime:
                    TimetableEditor.SetArrivalTime(userId, message);
                    break;
                case (int) AdminCommandStep.SetTripColumnEdit:
                    TimetableEditor.ChooseTripIdToEdit(userId, message);
                    break;
                case (int) AdminCommandStep.SetTripColumnDel:
                    TimetableEditor.ChooseTripIdToDelete(userId, message);
                    break;
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