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
        
        public static int[] GetAllAdminId()
        {
            var admins = new List<int>();
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = @"SELECT UserId FROM AdminInfo WHERE IsAdmin = 1";
                SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    admins.Add(Convert.ToInt32(sqLiteDataReader[0]));
                    admins.Sort();
                }
                sqLiteDataReader.Close();
                connection.Close();
                return admins.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return admins.ToArray();
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
                sqLiteCommand.CommandText = 
                    "INSERT INTO AdminInfo VALUES(@UserId, @IsAdmin, @CommandId, @ForwardingMessageId, @TripId, @Column, @TargetName)";
                sqLiteCommand.Parameters.AddWithValue("@UserId", userId);
                sqLiteCommand.Parameters.AddWithValue("@IsAdmin", true);
                sqLiteCommand.Parameters.AddWithValue("@CommandId", 0);
                sqLiteCommand.Parameters.AddWithValue("@ForwardingMessageId", 0);
                sqLiteCommand.Parameters.AddWithValue("@TripId", 0);
                sqLiteCommand.Parameters.AddWithValue("@Column", null);
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
                    "INSERT INTO Timetable VALUES(@TripId, @DeparturePlace, @DepartureDate, @DepartureTime, @ArrivalPlace, @ArrivalDate, @ArrivalTime)";
                sqLiteCommand.Parameters.AddWithValue("@TripId", null);
                sqLiteCommand.Parameters.AddWithValue("@DeparturePlace", "DeparturePlace");
                sqLiteCommand.Parameters.AddWithValue("@DepartureDate", "DepartureDate");
                sqLiteCommand.Parameters.AddWithValue("@DepartureTime", "DepartureTime");
                sqLiteCommand.Parameters.AddWithValue("@ArrivalPlace", "ArrivalPlace");
                sqLiteCommand.Parameters.AddWithValue("@ArrivalDate", "ArrivalDate");
                sqLiteCommand.Parameters.AddWithValue("@ArrivalTime", "ArrivalTime");
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static int GetLastTripId()
        {
            var trips = new List<int>();
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = @"SELECT TripId FROM Timetable";
                SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    trips.Add(Convert.ToInt32(sqLiteDataReader[0]));
                    trips.Sort();
                }
                sqLiteDataReader.Close();
                connection.Close();
                return trips[^1];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public static int GetTripId(int userId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT TripId FROM AdminInfo WHERE UserId = {userId}";
                var tripId = Convert.ToInt32(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return tripId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }
        }

        public static bool CheckTripId(int tripId)
        {
            var trips = new List<int>();
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = @"SELECT TripId FROM Timetable";
                SQLiteDataReader sqLiteDataReader = sqLiteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    trips.Add(Convert.ToInt32(sqLiteDataReader[0]));
                    trips.Sort();
                }
                sqLiteDataReader.Close();
                connection.Close();
                if (trips.Contains(tripId))
                    return true;

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
        
        public static void SetTripId(int userId, int tripId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"UPDATE AdminInfo Set TripId = {tripId} WHERE UserId = {userId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static string GetColumn(int userId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText =
                    $"SELECT Column FROM AdminInfo WHERE UserId = {userId}";
                var column = Convert.ToString(sqLiteCommand.ExecuteScalar());
                connection.Close();
                return column;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static void SetColumn(int userId, string column)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                var columnSqL = "\"" + column + "\"";
                sqLiteCommand.CommandText = $"UPDATE AdminInfo Set Column = {columnSqL} WHERE UserId = {userId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public static void UpdateTripColumn(int tripId, string column, string content)
        {
            try
            {
                var contentSqL = "\"" + content + "\"";
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"UPDATE Timetable Set {column} = {contentSqL} WHERE TripId = {tripId}";
                sqLiteCommand.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string[] GetTimetableTripsWithId()
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
                    var tripId = sqLiteDataReader.GetInt32(sqLiteDataReader.GetOrdinal("TripId"));
                    var departurePlace = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("DeparturePlace"));
                    var departureDate = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("DepartureDate"));
                    var departureTime = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("DepartureTime"));
                    var arrivalPlace = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("ArrivalPlace"));
                    var arrivalDate = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("ArrivalDate"));
                    var arrivalTime = sqLiteDataReader.GetString(sqLiteDataReader.GetOrdinal("ArrivalTime"));

                    trips.Add($"ID: {tripId} \n{numbTrip}. {departurePlace}—{arrivalPlace}" +
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

        public static void DeleteTrip(int tripId)
        {
            try
            {
                var connection = DataBaseContext.ConnectSqLite();
                connection.Open();
                SQLiteCommand sqLiteCommand = connection.CreateCommand();
                sqLiteCommand.CommandText = $"DELETE FROM Timetable WHERE TripId = {tripId}";
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