using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineMenu : InlinePanel
    {
        protected override async void CreateInlinePanel(MessageEventArgs messageEventArgs)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Главная")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Бренды")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Каталоги и прайсы")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Акции")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("О компании")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Новости")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Контакты")
                }
            });

            var message = await BotLogic.Bot.SendPhotoAsync(messageEventArgs.Message.From.Id,
                "https://i.imgur.com/mbchaO0.png", "Меню:",
                replyMarkup: inlineKeyboard);
            
            DataBaseContext.SaveMessageId(messageEventArgs, message.MessageId);
        }

        
    }
}