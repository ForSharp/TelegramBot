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

            
            switch (commandId)
            {
                case (int) AdminCommandStep.Default:
                    if (messageEventArgs.Message.Text == "/a")
                    {
                        AdminCommand.ShowUsers(userId);
                    }
                    break;
                case (int) AdminCommandStep.ShowUsers:
                    var tempUserName = messageEventArgs.Message.Text;
                    DataBaseContext.SetTargetName(userId, tempUserName);
                    
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        await BotController.Bot.SendTextMessageAsync(userId, "Введите username");
                    }
                    if (messageEventArgs.Message.Text == "Назад")
                    {
                        Undo(userId);
                    }
                    if (messageEventArgs.Message.Text == "Отмена")
                    {
                        Undo(userId);
                    }
                    if (messageEventArgs.Message.Text != "Подтвердить")
                    {
                        AdminCommand.ConfirmUser(userId, tempUserName);
                    }
                    break;
                case (int) AdminCommandStep.ConfirmUser:
                    if (messageEventArgs.Message.Text == "Подтвердить")
                    {
                        AdminCommand.AppointAdmin(messageEventArgs, DataBaseContext.GetTargetName(userId));
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
                        Undo(userId);
                    }
                    else
                    {
                        Undo(userId);
                    }
                    break;
            }
        }

        private static async void Undo(int userId)
        {
            await BotController.Bot.SendTextMessageAsync(userId, "Смена кнопок",
                replyMarkup: TextMessageProcessor.CreateDefaultButtons());
            DataBaseContext.SetCommandId(userId, (int) AdminCommandStep.Default);
        }
    }
}