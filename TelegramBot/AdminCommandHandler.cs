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
            
            var commandId = DataBaseContextAdmin.GetCommandId(userId);

            var keyWords = new[] {"Подтвердить", "Отмена", "Назад"};
            
            switch (commandId)
            {
                case (int) AdminCommandStep.Default:
                    if (messageEventArgs.Message.Text == "/a")
                    {
                        AdminCommand.ShowUsers(userId);
                    }

                    if (messageEventArgs.Message.Text == "/s")
                    {
                        AdminCommand.GetForwardingMessage(userId);
                    }
                    
                    if (messageEventArgs.Message.Text == "/t")
                    {
                        AdminCommand.EditTimetable(userId);
                    }
                    break;
                case (int) AdminCommandStep.ShowUsers:
                    var tempUserName = messageEventArgs.Message.Text;
                    DataBaseContextAdmin.SetTargetName(userId, tempUserName);
                    
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите username");
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        Undo(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        Undo(userId);
                    }
                    if (!keyWords.Contains(messageEventArgs.Message.Text))
                    {
                        AdminCommand.ConfirmUser(userId, tempUserName);
                    }
                    break;
                case (int) AdminCommandStep.ConfirmUser:
                    
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        AdminCommand.AppointAdmin(messageEventArgs, DataBaseContextAdmin.GetTargetName(userId));
                        
                        Thread.Sleep(10);
                        Undo(userId);
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        AdminCommand.ShowUsers(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        Undo(userId);
                    }
                    break;
                case (int) AdminCommandStep.SendMessage:
                    var forwardingMessage = messageEventArgs.Message;
                    DataBaseContextAdmin.SetForwardingMessageId(userId, forwardingMessage.MessageId);
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите сообщение для рассылки.");
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        Undo(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        Undo(userId);
                    }
                    if (!keyWords.Contains(messageEventArgs.Message.Text))
                    {
                        AdminCommand.ConfirmForwardingMessage(userId, forwardingMessage.MessageId);
                    }
                    break;
                case (int) AdminCommandStep.ConfirmSending:
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        var usersId = DataBaseContextAdmin.GetAllUserId();
                        foreach (var targetId in usersId)
                        {
                            AdminCommand.ForwardMessage(targetId, userId, DataBaseContextAdmin.GetForwardingMessageId(userId));
                        }
                        
                        Thread.Sleep(10);
                        Undo(userId);
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        AdminCommand.GetForwardingMessage(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        Undo(userId);
                    }
                    break;
                case (int) AdminCommandStep.EditTimetable:
                    if (messageEventArgs.Message.Text == "Добавить рейс")
                    {
                        DataBaseContextAdmin.AddTrip();
                        DataBaseContextAdmin.SetTripId(userId, DataBaseContextAdmin.GetLastTripId());
                        DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DeparturePlace);
                        await BotController.Bot.SendTextMessageAsync(userId, "Откуда рейс?", replyMarkup: CreateSimpleKeyboard());
                    }
                    break;
                case (int) AdminCommandStep.DeparturePlace:
                    try
                    {
                        var temp = messageEventArgs.Message.Text;
                        if (messageEventArgs.Message.Text == "Назад")
                        {
                            AdminCommand.EditTimetable(userId);
                        }
                        if (messageEventArgs.Message.Text == "Отмена")
                        {
                            Undo(userId);
                        }
                        if (!keyWords.Contains(messageEventArgs.Message.Text))
                        {
                            DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), 
                                AdminCommandStep.DeparturePlace.ToString(), temp);
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DepartureDate);
                            await BotController.Bot.SendTextMessageAsync(userId, "Дата отправки?");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case (int) AdminCommandStep.DepartureDate:
                    try
                    {
                        var temp = messageEventArgs.Message.Text;
                        if (messageEventArgs.Message.Text == "Назад")
                        {
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DeparturePlace);
                        }
                        if (messageEventArgs.Message.Text == "Отмена")
                        {
                            Undo(userId);
                        }
                        if (!keyWords.Contains(messageEventArgs.Message.Text))
                        {
                            DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), 
                                AdminCommandStep.DepartureDate.ToString(), temp);
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DepartureTime);
                            await BotController.Bot.SendTextMessageAsync(userId, "Время отправки?");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case (int) AdminCommandStep.DepartureTime:
                    try
                    {
                        var temp = messageEventArgs.Message.Text;
                        if (messageEventArgs.Message.Text == "Назад")
                        {
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DepartureDate);
                        }
                        if (messageEventArgs.Message.Text == "Отмена")
                        {
                            Undo(userId);
                        }
                        if (!keyWords.Contains(messageEventArgs.Message.Text))
                        {
                            DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), 
                                AdminCommandStep.DepartureTime.ToString(), temp);
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalPlace);
                            await BotController.Bot.SendTextMessageAsync(userId, "Место прибытия?");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case (int) AdminCommandStep.ArrivalPlace:
                    try
                    {
                        var temp = messageEventArgs.Message.Text;
                        if (messageEventArgs.Message.Text == "Назад")
                        {
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DepartureTime);
                        }
                        if (messageEventArgs.Message.Text == "Отмена")
                        {
                            Undo(userId);
                        }
                        if (!keyWords.Contains(messageEventArgs.Message.Text))
                        {
                            DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), 
                                AdminCommandStep.ArrivalPlace.ToString(), temp);
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalDate);
                            await BotController.Bot.SendTextMessageAsync(userId, "Дата прибытия?");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case (int) AdminCommandStep.ArrivalDate:
                    try
                    {
                        var temp = messageEventArgs.Message.Text;
                        if (messageEventArgs.Message.Text == "Назад")
                        {
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalPlace);
                        }
                        if (messageEventArgs.Message.Text == "Отмена")
                        {
                            Undo(userId);
                        }
                        if (!keyWords.Contains(messageEventArgs.Message.Text))
                        {
                            DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), 
                                AdminCommandStep.ArrivalDate.ToString(), temp);
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalTime);
                            await BotController.Bot.SendTextMessageAsync(userId, "Время прибытия?");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case (int) AdminCommandStep.ArrivalTime:
                    try
                    {
                        var temp = messageEventArgs.Message.Text;
                        if (messageEventArgs.Message.Text == "Назад")
                        {
                            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalDate);
                        }
                        if (messageEventArgs.Message.Text == "Отмена")
                        {
                            Undo(userId);
                        }
                        if (!keyWords.Contains(messageEventArgs.Message.Text))
                        {
                            DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), 
                                AdminCommandStep.ArrivalTime.ToString(), temp);
                            AdminCommand.EditTimetable(userId);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
            }
        }

        private static async void Undo(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, "Для вывода команд введите /admin",
                replyMarkup: TextMessageProcessor.CreateDefaultButtons());
            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
        }
        
        private static ReplyKeyboardMarkup CreateSimpleKeyboard()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Назад"),
                    new KeyboardButton("Отмена")
                }
            }, true, true);

            return replyKeyboard;
        }
    }
}