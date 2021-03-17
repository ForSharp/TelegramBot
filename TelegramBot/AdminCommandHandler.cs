using System.Threading;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class AdminCommandHandler
    {
        public static async void HandleAdminCommands(MessageEventArgs messageEventArgs)
        {
            //проверить админность

            var userId = messageEventArgs.Message.From.Id;
            
            var commandId = DataBaseContext.GetCommandId(userId);

            string tempUserName = "";

            switch (commandId)
            {
                case (int) AdminCommandStep.Default:
                    if (messageEventArgs.Message.Text == "/a")
                    {
                        AdminCommand.ShowUsers(userId);
                    }
                    break;
                case (int) AdminCommandStep.ShowUsers:
                    tempUserName = messageEventArgs.Message.Text;
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите username");
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, ".",
                            replyMarkup: TextMessageProcessor.CreateDefaultButtons());
                        DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.Default);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, ".",
                            replyMarkup: TextMessageProcessor.CreateDefaultButtons());
                        DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.Default);
                    }
                    if (messageEventArgs.Message.Text != "Подтвердить")
                    {
                        AdminCommand.ConfirmUser(userId, tempUserName);
                    }
                    break;
                case (int) AdminCommandStep.ConfirmUser:
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        AdminCommand.AppointAdmin(messageEventArgs, tempUserName);
                        
                        Thread.Sleep(10);
                        TextMessageProcessor.CreateDefaultButtons();
                        DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.Default);
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        AdminCommand.ShowUsers(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, ".",
                            replyMarkup: TextMessageProcessor.CreateDefaultButtons());
                        DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.Default);
                    }
                    else
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, ".",
                            replyMarkup: TextMessageProcessor.CreateDefaultButtons());
                        DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.Default);
                    }
                    break;
            }
        }
    }
}