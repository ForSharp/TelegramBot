using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class TimetableEditor
    {
        private static readonly string[] KeyWords = {"Подтвердить", "Отмена", "Назад"};

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
                    $"Какие действия произвести с текущим расписанием?" +
                    $"{ShowTimetableWithId()}", replyMarkup: replyKeyboard);
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.EditTimetable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static string ShowTimetableWithId()
        {
            try
            {
                string timetable = String.Join(null, DataBaseContextAdmin.GetTimetableTripsWithId());

                return $"\n\n{timetable}";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return " ";
            }
            
        }


        public static async void ChooseTripIdToEdit(int userId)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static async void ChooseTripIdToDelete(int userId)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async void EditTripColumn(int userId, string command, string message)
        {
            var keyCommands = new Dictionary<string, string>();
            keyCommands.Add("Место отправки", AdminCommandStep.ArrivalPlace.ToString());
            keyCommands.Add("Дата отправки", AdminCommandStep.ArrivalDate.ToString());
            keyCommands.Add("Время отправки", AdminCommandStep.ArrivalTime.ToString());
            keyCommands.Add("Место прибытия", AdminCommandStep.DeparturePlace.ToString());
            keyCommands.Add("Дата прибытия", AdminCommandStep.DepartureDate.ToString());
            keyCommands.Add("Время прибытия", AdminCommandStep.DepartureTime.ToString());

            if (keyCommands.ContainsKey(command))
            {
                
            }
            
        }
        
        public static async void SetDeparturePlace(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    EditTimetable(userId);
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
                    await BotController.Bot.SendTextMessageAsync(userId, "Откуда рейс?", 
                        replyMarkup:KeyboardContainer.CreateTwoKeyboardAdminButtons());
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
                    EditTimetable(userId);
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
                replyMarkup: KeyboardContainer.CreateDefaultKeyboard());
            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
        }
    }
}