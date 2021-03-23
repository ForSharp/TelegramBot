using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public static class TimetableEditor
    {
        private static readonly string[] KeyWords = {"Подтвердить", "Отмена", "Назад"};

        public static async void StartEditTimetable(int userId)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId,
                    $"Какие действия произвести с текущим расписанием?" +
                    $"{ShowTimetableWithId()}", replyMarkup: KeyboardContainer.CreateTimetableEditKeyboard());
            
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.EditTimetable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async void EditTimetable(int userId, string message)
        {
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
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SetTripIdEdit);
            }
            if (message == "Удалить рейс")
            {
                await BotController.Bot.SendTextMessageAsync(userId, "Введите ID рейса для удаления.",
                    replyMarkup: KeyboardContainer.CreateTwoKeyboardAdminButtons());
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SetTripIdDel);
            }
            if (message == "Отмена")
            {
                Undo(userId);
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
        
        public static async void ChooseTripIdToEdit(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    StartEditTimetable(userId);
                }
                else if (message == "Отмена") 
                {
                    Undo(userId);
                }
                else if (!int.TryParse(temp, out var tempInt))
                {
                    await BotController.Bot.SendTextMessageAsync(userId, "Введите число, которое является ID рейса.");
                }
                else 
                {
                    if (!DataBaseContextAdmin.CheckTripId(tempInt))
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите существующий ID рейса.");
                    }
                    DataBaseContextAdmin.SetTripId(userId, tempInt);
                    await BotController.Bot.SendTextMessageAsync(userId, "Выберите, что будете редактировать.", 
                        replyMarkup: KeyboardContainer.CreateTimetableEditTripKeyboard());
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ChooseTripColumn);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static async void ChooseTripIdToDelete(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    StartEditTimetable(userId);
                }
                else if (message == "Отмена")
                {
                    Undo(userId);
                }
                else if (!int.TryParse(temp, out var tempInt))
                {
                    await BotController.Bot.SendTextMessageAsync(userId, "Введите число, которое является ID рейса.");
                }
                else if (!KeyWords.Contains(message))
                {
                    if (!DataBaseContextAdmin.CheckTripId(tempInt))
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите существующий ID рейса.");
                    }
                    DataBaseContextAdmin.DeleteTrip(tempInt);
                    StartEditTimetable(userId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static async void ChooseTripColumn(int userId, string message)
        {
            try
            {
                var keyCommands = new Dictionary<string, string>();
                keyCommands.Add("Место отправки", AdminCommandStep.DeparturePlace.ToString()); 
                keyCommands.Add("Дата отправки", AdminCommandStep.DepartureDate.ToString()); 
                keyCommands.Add("Время отправки", AdminCommandStep.DepartureTime.ToString()); 
                keyCommands.Add("Место прибытия", AdminCommandStep.ArrivalPlace.ToString()); 
                keyCommands.Add("Дата прибытия", AdminCommandStep.ArrivalDate.ToString()); 
                keyCommands.Add("Время прибытия", AdminCommandStep.ArrivalTime.ToString()); 

                if (keyCommands.ContainsKey(message))
                {
                    keyCommands.TryGetValue(message, out var resMessage);
                    DataBaseContextAdmin.SetColumn(userId, resMessage);
                    await BotController.Bot.SendTextMessageAsync(userId, "Введите новое значение.", 
                        replyMarkup: KeyboardContainer.CreateTwoKeyboardAdminButtons());
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.EditTripColumn);
                }
                else if (message == "Назад")
                {
                    await BotController.Bot.SendTextMessageAsync(userId, "Введите ID рейса для редактирования.", 
                        replyMarkup: KeyboardContainer.CreateTwoKeyboardAdminButtons());
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.SetTripIdEdit);
                }
                else if (message == "Отмена") 
                {
                    Undo(userId);
                }
                else
                {
                    await BotController.Bot.SendTextMessageAsync(userId, "Выберите, что будете редактировать.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static async void EditTripColumn(int userId, string message)
        {
            try
            {
                if (message == "Назад")
                {
                    await BotController.Bot.SendTextMessageAsync(userId, "Выберите, что будете редактировать.", 
                        replyMarkup: KeyboardContainer.CreateTimetableEditTripKeyboard());
                    DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.ChooseTripColumn);
                }
                else if (message == "Отмена") 
                {
                    Undo(userId);
                }
                else
                {
                    DataBaseContextAdmin.UpdateTripColumn(DataBaseContextAdmin.GetTripId(userId), DataBaseContextAdmin.GetColumn(userId), message);
                    StartEditTimetable(userId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static async void SetDeparturePlace(int userId, string message)
        {
            try
            {
                var temp = message;
                if (message == "Назад")
                {
                    StartEditTimetable(userId);
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
                    await BotController.Bot.SendTextMessageAsync(userId, "Откуда рейс?");
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
                    StartEditTimetable(userId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static async void Undo(int userId)
        {
            try
            {
                await BotController.Bot.SendTextMessageAsync(userId, "Для вывода команд введите /admin",
                    replyMarkup: KeyboardContainer.CreateDefaultKeyboard());
                DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}