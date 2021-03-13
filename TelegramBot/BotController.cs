﻿using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class BotController
    {
        public static TelegramBotClient Bot;
        
        private static void Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            if (!DataConnection.CheckFileExistence())
            {
                DataConnection.CreateDataFile();
            }

            try
            {
                try
                {
                    try
                    {
                        Bot = new TelegramBotClient(DataConnection.GetTelegramBotToken()); 
                    }
                    catch (DataNotFoundException e)
                    {
                        Console.WriteLine("Токен не найден." + e);
                    }
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Неверный формат токена." + e);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            Bot.OnMessage += BotOnMessage;

            Bot.OnCallbackQuery += BotOnCallbackQuery;
            
            var user = Bot.GetMeAsync().Result;
            Console.WriteLine(user.FirstName);
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static void BotOnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            var senderName = GetAvailableSenderName(messageEventArgs);
            
            MessageHandler.HandleSenderMessage(senderName, messageEventArgs);
            
        }

        private static void BotOnCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            
        }

        /* Senders may not have last name and username.
         * The method gets the most complete sender name.
         */
        private static string GetAvailableSenderName(MessageEventArgs messageEventArgs)
        {
            string senderName;
            if (messageEventArgs.Message.From.LastName == null && messageEventArgs.Message.From.Username == null)
            {
                senderName = $"{messageEventArgs.Message.From.FirstName}";
                return senderName;
            }
            if (messageEventArgs.Message.From.LastName == null)
            {
                senderName = $"{messageEventArgs.Message.From.FirstName} {messageEventArgs.Message.From.Username}";
                return senderName;
            }
            if (messageEventArgs.Message.From.Username == null)
            {
                senderName = $"{messageEventArgs.Message.From.FirstName} {messageEventArgs.Message.From.LastName}";
                return senderName;
            }

            senderName = $"{messageEventArgs.Message.From.FirstName} {messageEventArgs.Message.From.LastName} {messageEventArgs.Message.From.Username}";
            return senderName;
        }
    }
}