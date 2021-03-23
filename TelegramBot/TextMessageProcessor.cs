using System;
using Telegram.Bot.Args;
using ApiAiSDK;
using TelegramBot.InlinePanels;

namespace TelegramBot
{
    public static class TextMessageProcessor
    {
        private static async void CreateKeyboardButtons(int userId, string firstName)
        {
            if (DataBaseContextAdmin.GetCommandId(userId) != (int) AdminCommandStep.Default)
                return;
            
            try
            {
                var replyKeyboard = KeyboardContainer.CreateDefaultKeyboard();
                
                await BotController.Bot.SendTextMessageAsync(userId, $"Здравствуйте, " + 
                       $"{firstName}! \nПожалуйста, воспользуйтесь кнопками для начала работы", 
                    replyMarkup: replyKeyboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        public static void CreateStartStatement(MessageEventArgs messageEventArgs)
        {
            var userId = messageEventArgs.Message.From.Id;
            var firstName = messageEventArgs.Message.From.FirstName;
            CreateKeyboardButtons(userId, firstName);
            var inlineMenu = new InlineMenu();
            inlineMenu.RunCreatingProcess(messageEventArgs, true);
        }

        public static async void ShowInTheMap(int userId)
        {
            await BotController.Bot.SendLocationAsync(userId, 45.0386671f, 39.066130f);
        }

        public static async void SendContacts(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, @"ООО ""Планета Групп""
Страна: Россия
Регион: Краснодарский край
Индекс: 350059
Адрес: 
г.Краснодар, ул.Новороссийская, 238/1
Телефоны:
8(861)204-00-13
8(928)454-00-13
E-mail: info@planeta-grupp.ru
Время работы:
пн-чт: 8:30-17:30
пт:    8:30-16:30
сб-вс: выходной");
        }

        public static async void GetUserNumber(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, "GetUserNumber");
        }

        public static async void SendAiAnswer(int userId, string message)
        {
            var aiConfiguration = new AIConfiguration("ffbc6c13dab14a87b2ef26a94d1014f9", SupportedLanguage.Russian);
            var apiAi = new ApiAi(aiConfiguration);
            try
            {
                var response = apiAi.TextRequest(message);
                var messageReply = response.Result.Fulfillment.Speech;
                if (messageReply == "")
                    messageReply = "Я вас не понял.";
                await BotController.Bot.SendTextMessageAsync(userId, messageReply);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}