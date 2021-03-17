namespace TelegramBot
{
    public enum AdminCommandStep
    {
        Default = 0,
        ShowUsers = 100,
        ConfirmUser = 101,
        SendMessage = 200,
        ConfirmSending = 201,
        
    }
}