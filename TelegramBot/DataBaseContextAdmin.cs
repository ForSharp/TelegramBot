﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TelegramBot
{
    public static class DataBaseContextAdmin 
    {
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
                sqLiteCommand.CommandText = "INSERT INTO AdminInfo VALUES(@UserId, @IsAdmin, @CommandId, @TargetName)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", userId);
                sqLiteCommand.Parameters.AddWithValue("@IsAdmin", true);
                sqLiteCommand.Parameters.AddWithValue("@CommandId", 0);
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
    }
}