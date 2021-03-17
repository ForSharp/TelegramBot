using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Telegram.Bot.Args;

namespace TelegramBot

{
    public static class DataBaseContext
    {
        private static SQLiteConnection _connectionDataBase;

        private static SQLiteConnection ConnectSqLite()
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

        public static string[] GetAllUserNames()
        {
            var users = new List<string>();
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $@"SELECT UserName FROM RegUsers";
                SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    if (Convert.ToString(sqLiteDataReader[0].ToString()) != "")
                    {
                        users.Add(Convert.ToString(sqLiteDataReader[0].ToString()));
                    }
                    users.Sort();
                }
                sqLiteDataReader.Close();
                connection.Close();
                return users.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return users.ToArray();
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

        public static void SaveMessageId(int userId, int messageId)
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

        public static void ChangeUserStatus(string userName)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                var userId = GetUserIdByUserName(userName);
                if (userId == 0)
                {
                    throw new Exception();
                }
                sqLiteCommand.CommandText = "INSERT INTO AdminInfo VALUES(@UserId, @IsAdmin, @CommandId)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", userId);
                sqLiteCommand.Parameters.AddWithValue("@IsAdmin", true);
                sqLiteCommand.Parameters.AddWithValue("@CommandId", null);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static int GetUserIdByUserName(string userName)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT UserId FROM RegUsers WHERE UserName = {userName}";
                var userId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
                return userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public static int GetCommandId(int userId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT CommandId FROM AdminInfo WHERE UserId = {userId}";
                var stepId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return stepId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public static void SetCommandId(int userId, int commandId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"UPDATE AdminInfo Set CommandId = {commandId} WHERE UserId = {userId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    }
}