using System;
using System.Data.SQLite;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class DataBaseContext
    {
        private static SQLiteConnection _connectionDataBase;

        private static SQLiteConnection ConnectSqLite()
        {
            if (_connectionDataBase == null)
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
                    sqLiteCommand.Parameters.AddWithValue("@LastName", messageEventArgs.Message.From.LastName);
                if (messageEventArgs.Message.From.Username != null)
                    sqLiteCommand.Parameters.AddWithValue("@Username", messageEventArgs.Message.From.Username);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
                
                Console.WriteLine("Пользователь зарегистрирован");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void DeleteOldPanel(MessageEventArgs messageEventArgs)
        {
            var connection = ConnectSqLite();
            connection.Open();
            SQLiteCommand sqLiteCommand = connection.CreateCommand();
            sqLiteCommand.CommandText = $"INSERT INTO UsersInfo WHERE UserId IS {messageEventArgs.Message.From.Id}";
            connection.Close();
        }

        public static void SaveMessageId(MessageEventArgs messageEventArgs, int messageId)
        {
            var connection = ConnectSqLite();
            connection.Open();
            SQLiteCommand sqLiteCommand = connection.CreateCommand();
            sqLiteCommand.CommandText = $"INSERT INTO UsersInfo VALUES(@MessageId) WHERE UserId IS {messageEventArgs.Message.From.Id}";
            sqLiteCommand.Parameters.AddWithValue("@MessageId", messageId);
            sqLiteCommand.ExecuteNonQuery();
            connection.Close();
        }
        
    
    }
}