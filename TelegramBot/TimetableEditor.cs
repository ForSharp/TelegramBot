using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class TimetableEditor
    {
        private static readonly string[] KeyWords = {"Подтвердить", "Отмена", "Назад"};

        private static readonly Dictionary<string, string> KeyCommands = new Dictionary<string, string>();

        
        public static async void SetDeparturePlace(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    AdminCommand.EditTimetable(userId);
                }
                if (message == "Отмена")
                {
                    Undo(userId);
                }
                if (!KeyWords.Contains(message))
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
        }
        
        public static async void SetDepartureDate(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DeparturePlace);
                    await BotController.Bot.SendTextMessageAsync(userId, "Откуда рейс?", replyMarkup: AdminCommand.CreateSimpleKeyboard());
                }
                if (message == "Отмена")
                {
                    Undo(userId);
                }
                if (!KeyWords.Contains(message))
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
        }

        public static async void SetDepartureTime(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DepartureDate);
                    await BotController.Bot.SendTextMessageAsync(userId, "Дата отправки?");
                }
                if (message == "Отмена")
                {
                    Undo(userId);
                }
                if (!KeyWords.Contains(message))
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
        }
        
        public static async void SetArrivalPlace(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.DepartureTime);
                    await BotController.Bot.SendTextMessageAsync(userId, "Время отправки?");
                }
                if (message == "Отмена")
                {
                    Undo(userId);
                }
                if (!KeyWords.Contains(message))
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
        }
        
        public static async void SetArrivalDate(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalPlace);
                    await BotController.Bot.SendTextMessageAsync(userId, "Место прибытия?");
                }
                if (message == "Отмена")
                {
                    Undo(userId);
                }
                if (!KeyWords.Contains(message))
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
        }
        
        public static async void SetArrivalTime(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ArrivalDate);
                    await BotController.Bot.SendTextMessageAsync(userId, "Дата прибытия?");
                }
                if (message == "Отмена")
                {
                    Undo(userId);
                }
                if (!KeyWords.Contains(message))
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
        }

        private static async void Undo(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, "Для вывода команд введите /admin",
                replyMarkup: TextMessageProcessor.CreateDefaultButtons());
            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
        }
    }
}