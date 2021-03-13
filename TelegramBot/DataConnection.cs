using System;
using System.IO;

namespace TelegramBot
{
    public static class DataConnection
    {
        private const string DataFilePath = "DataFile.txt";
        private const string TokenInFile = "TelegramBotToken=";
        private const string PathInFile = "Data Source=";
        
        private static FileStream _dataFile;
        
        public static void CreateDataFile()
        {
            try
            {
                _dataFile = new FileStream(DataFilePath, FileMode.CreateNew);
                Console.WriteLine($"{DataFilePath} автоматически создан в папке с исполняемым файлом.");
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка создания файла." + e.Message);
                return;
            }
            finally
            {
                _dataFile.Close();
            }

            var dataFileOut = new StreamWriter(DataFilePath, true);

            try
            {
                dataFileOut.WriteLine(TokenInFile);
                dataFileOut.WriteLine(PathInFile);
                Console.WriteLine($"{DataFilePath} предназначен для хранения токена и пути к БД.");
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка создания файла." + e.Message);
            }
            finally
            {
                dataFileOut.Close();
            }
            
            FillInDataFile();
        }

        private static void FillInDataFile()
        {
            var dataFileOut = new StreamWriter(DataFilePath);
            
            try
            {
                Console.WriteLine($"Заполните следующие поля {DataFilePath}.");
                Console.WriteLine("Вы всегда сможете изменить их, открыв файл вручную.");
                
                Console.Write(TokenInFile);
                var telegramBotToken = Console.ReadLine();
                Console.Write(PathInFile);
                var pathToDataBase = Console.ReadLine();
                
                dataFileOut.WriteLine($"{TokenInFile}{telegramBotToken}");
                dataFileOut.WriteLine($"{PathInFile}{pathToDataBase}");
            }
            catch (IOException e)
            {
                Console.WriteLine("Ошибка изменения файла." + e.Message);
            }
            finally
            {
                dataFileOut.Close();
            }
        }
        
        public static bool CheckFileExistence()
        {
            return File.Exists(DataFilePath);
        }

        public static string GetTelegramBotToken()
        {
            string[] allLines = File.ReadAllLines(DataFilePath);
            foreach (var line in allLines)
            {
                if (line.StartsWith(TokenInFile))
                {
                    var found = line.IndexOf(TokenInFile, StringComparison.Ordinal);
                    return line.Substring(found + TokenInFile.Length);
                }
            }

            throw new DataNotFoundException();
        }

        public static string GetPathToDataBase()
        {
            string[] allLines = File.ReadAllLines(DataFilePath);
            foreach (var line in allLines)
            {
                if (line.StartsWith(PathInFile))
                {
                    var found = line.IndexOf(PathInFile, StringComparison.Ordinal);
                    return line.Substring(found);
                }
            }

            throw new DataNotFoundException();
        }

        public static FileStream GetImage(string imageName)
        {
            FileStream fileStream = new FileStream($@"Images\{imageName}.png", FileMode.Open, FileAccess.Read);
            return fileStream;
        }

     
    
    }
}