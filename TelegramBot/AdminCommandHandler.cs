using System.Threading;
using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class AdminCommandHandler
    {
        public static async void HandleAdminCommands(MessageEventArgs messageEventArgs)
        {
            if (!DataBaseContextAdmin.CheckAdminRights(messageEventArgs.Message.From.Id))
                return;

            var userId = messageEventArgs.Message.From.Id;
            
            var commandId = DataBaseContextAdmin.GetCommandId(userId);

            
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
                    DataBaseContextAdmin.SetTargetName(userId, tempUserName);
                    
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
                        AdminCommand.AppointAdmin(messageEventArgs, DataBaseContextAdmin.GetTargetName(userId));
                        Thread.Sleep(10);
                        TextMessageProcessor.CreateDefaultButtons();
                        DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
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
            DataBaseContextAdmin.SetCommandId(userId, (int) AdminCommandStep.Default);
        }
    }
}