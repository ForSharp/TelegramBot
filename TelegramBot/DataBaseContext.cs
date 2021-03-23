using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Telegram.Bot.Args;

namespace TelegramBot

{
    public static class DataBaseContext
    {
        private static SQLiteConnection _connectionDataBase;

        public static SQLiteConnection ConnectSqLite()
        {
            try
            {
                try
                {
                    _connectionDataBase = new SQLiteConnection(DataConnection.GetPathToDataBase());
                }
                catch (DataNotFoundException e)
                {
                    Console.WriteLine("Путь к базе данных не найден" + e);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return _connectionDataBase;
        }

        public static void RegisterUser(MessageEventArgs messageEventArgs)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = "INSERT INTO RegUsers VALUES(@UserId, @FirstName, @LastName, @Username)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", messageEventArgs.Message.From.Id);
                sqLiteCommand.Parameters.AddWithValue("@FirstName", messageEventArgs.Message.From.FirstName);
                if (messageEventArgs.Message.From.LastName != null)
                {
                    sqLiteCommand.Parameters.AddWithValue("@LastName", messageEventArgs.Message.From.LastName);
                }
                else
                {
                    sqLiteCommand.Parameters.AddWithValue("@LastName", null);
                }    
                if (messageEventArgs.Message.From.Username != null)
                {
                    sqLiteCommand.Parameters.AddWithValue("@Username", messageEventArgs.Message.From.Username);
                }
                else
                {
                    sqLiteCommand.Parameters.AddWithValue("@Username", null);
                }
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
                
                Console.WriteLine("Пользователь зарегистрирован.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        public static int GetMessageId(int userId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT MessageId FROM UsersInfo WHERE UserId = {userId}";
                var messageId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return messageId;
            }
            catch (Exception)
            {
                return 0;
                //Console.WriteLine(e.Message);
                //Ignored
            }
        }

        public static void SetMessageId(int userId, int messageId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = "INSERT INTO UsersInfo VALUES(@UserId, @MessageId, @StepId)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", userId);
                sqLiteCommand.Parameters.AddWithValue("@MessageId", messageId);
                sqLiteCommand.Parameters.AddWithValue("@StepId", (int)InlinePanelStep.Menu);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void SetStepId(int userId, int stepId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"UPDATE UsersInfo Set StepId = {stepId} WHERE UserId = {userId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static int GetStepId(CallbackQueryEventArgs callbackQueryEventArgs)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT StepId FROM UsersInfo WHERE UserId = {callbackQueryEventArgs.CallbackQuery.From.Id}";
                var stepId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return stepId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        
        public static string[] GetTimetableTrips()
        {
            var numbTrip = 0;
            var trips = new List<string>();
            
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT TripId, DeparturePlace, DepartureDate, DepartureTime, ArrivalPlace, ArrivalDate, ArrivalTime FROM Timetable ORDER BY TripId ASC";
                SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    numbTrip++;
                    var departurePlace = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("DeparturePlace"));
                    var departureDate = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("DepartureDate"));
                    var departureTime = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("DepartureTime"));
                    var arrivalPlace = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("ArrivalPlace"));
                    var arrivalDate = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("ArrivalDate"));
                    var arrivalTime = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("ArrivalTime"));

                    trips.Add($"{numbTrip}. {departurePlace}—{arrivalPlace}" +
                              $"\nДата Отправки: {departureDate} [{departureTime}]" +
                              $"\nДата Прибытия: {arrivalDate} [{arrivalTime}]\n\n");
                }
                sqLiteDataReader.Close();
                connection.Close();

                return trips.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return trips.ToArray();
            }
        }
    }
}