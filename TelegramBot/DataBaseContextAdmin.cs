using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TelegramBot
{
    public static class DataBaseContextAdmin 
    {
        public static bool CheckAdminRights(int userId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT IsAdmin FROM AdminInfo WHERE UserId = {userId}";
                var isAdmin = Convert.ToBoolean(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return isAdmin;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public static string[] GetAllUserNames()
        {
            var users = new List<string>();
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = @"SELECT UserName FROM RegUsers";
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

        public static int[] GetAllUserId()
        {
            var users = new List<int>();
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = @"SELECT UserId FROM RegUsers";
                SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    users.Add(Convert.ToInt32(sqLiteDataReader[0]));
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
        
        public static void ChangeUserStatus(string userName)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                var userId = GetUserIdByUserName(userName);
                if (userId == 0)
                {
                    throw new Exception();
                }
                sqLiteCommand.CommandText = "INSERT INTO AdminInfo VALUES(@UserId, @IsAdmin, @CommandId, @ForwardingMessageId, @TargetName)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", userId);
                sqLiteCommand.Parameters.AddWithValue("@IsAdmin", true);
                sqLiteCommand.Parameters.AddWithValue("@CommandId", 0);
                sqLiteCommand.Parameters.AddWithValue("@ForwardingMessageId", 0);
                sqLiteCommand.Parameters.AddWithValue("@TargetName", null);
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
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                var userNameSqL = "\"" + userName + "\"";
                sqLiteCommand.CommandText =
                    $"SELECT UserId FROM RegUsers WHERE UserName = {userNameSqL}";
                var userId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public static string GetTargetName(int userId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT TargetName FROM AdminInfo WHERE UserId = {userId}";
                var targetName = Convert.ToString(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return targetName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static void SetTargetName(int userId, string targetName)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                var targetNameSqL = "\"" + targetName + "\"";
                sqLiteCommand.CommandText = $"UPDATE AdminInfo Set TargetName = {targetNameSqL} WHERE UserId = {userId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static int GetCommandId(int userId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
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
                var connection = DataBaseContext.ConnectSqLite();
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
        
        public static int GetForwardingMessageId(int userId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT ForwardingMessageId FROM AdminInfo WHERE UserId = {userId}";
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
        
        public static void SetForwardingMessageId(int userId, int forwardingMessageId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"UPDATE AdminInfo Set ForwardingMessageId = {forwardingMessageId} WHERE UserId = {userId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void AddTrip()
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = 
                    "INSERT INTO Timetable VALUES(@Id, @DeparturePlace, @DepartureDate, @DepartureTime, @ArrivalPlace, @ArrivalDate, @ArrivalTime)";
                sqLiteCommand.Parameters.AddWithValue("@Id", null);
                sqLiteCommand.Parameters.AddWithValue("@DeparturePlace", null);
                sqLiteCommand.Parameters.AddWithValue("@DepartureDate", null);
                sqLiteCommand.Parameters.AddWithValue("@DepartureTime", null);
                sqLiteCommand.Parameters.AddWithValue("@ArrivalPlace", null);
                sqLiteCommand.Parameters.AddWithValue("@ArrivalDate", null);
                sqLiteCommand.Parameters.AddWithValue("@ArrivalTime", null);
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void DeleteTrip()
        {
            
        }
    }
}