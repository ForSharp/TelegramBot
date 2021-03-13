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
                    sqLiteCommand.Parameters.AddWithValue("@LastName", messageEventArgs.Message.From.LastName);
                if (messageEventArgs.Message.From.Username != null)
                    sqLiteCommand.Parameters.AddWithValue("@Username", messageEventArgs.Message.From.Username);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
                
                Console.WriteLine("Пользователь зарегистрирован");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static int GetMessageId(MessageEventArgs messageEventArgs)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT MessageId FROM UsersInfo WHERE UserId = {messageEventArgs.Message.From.Id}";
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

        public static void SaveMessageId(MessageEventArgs messageEventArgs, int messageId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = "INSERT INTO UsersInfo VALUES(@UserId, @MessageId, @StepId)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", messageEventArgs.Message.From.Id);
                sqLiteCommand.Parameters.AddWithValue("@MessageId", messageId);
                sqLiteCommand.Parameters.AddWithValue("@StepId", (int)InlinePanelSteps.Menu);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void SetStepId(MessageEventArgs messageEventArgs, int stepId)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"UPDATE UsersInfo Set StepId = {stepId} WHERE UserId = {messageEventArgs.Message.From.Id}";
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static int GetStepId(MessageEventArgs messageEventArgs)
        {
            try
            {
                var connection = ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT StepId FROM UsersInfo WHERE UserId = {messageEventArgs.Message.From.Id}";
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
        

    }
}