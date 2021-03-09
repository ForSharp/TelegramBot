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
            //if (_connectionDataBase == null)
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


        //public static Action<MessageEventArgs> DeleteAction = (DeleteOldPanel);
        public static async void DeleteOldPanel(MessageEventArgs messageEventArgs)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"SELECT MessageId FROM UsersInfo WHERE UserId IS {messageEventArgs.Message.From.Id}";
                int messageId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                await BotLogic.Bot.DeleteMessageAsync(messageEventArgs.Message.From.Id, messageId);
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public static void SaveMessageId(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = "INSERT INTO UsersInfo VALUES(@UserId, @MessageId)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", messageEventArgs.Message.From.Id);
                sqLiteCommand.Parameters.AddWithValue("@MessageId", messageId);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}