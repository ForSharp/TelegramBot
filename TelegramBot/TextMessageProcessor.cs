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
            await BotController.Bot.SendLocationAsync(userId, 45.033445f, 39.045229f);
        }

        public static async void SendContacts(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, @"ООО ""Планета Групп""
Страна: Россия
Регион: Краснодарский край
Индекс: 350059

Адрес: 
г.Краснодар, ул. Уральская, 75к2, 
помещение 202,204

Телефоны:
+79298455400
+78612040013
+79284540013

E-mail: info@planeta-grupp.ru

Время работы:
пн-чт: 8:30-17:30
пт:    8:30-16:30
сб-вс: выходной");
        }

        public static async void GetUserNumber(MessageEventArgs messageEventArgs, int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, "GetUserNumber");
            
            //var phoneNumber = messageEventArgs.Message.Contact.PhoneNumber;
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