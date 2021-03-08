using System.Threading.Tasks;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineMenu : InlinePanel
    {
        protected override void CreateInlinePanel(MessageEventArgs messageEventArgs, out int message)
        {
            var inlineKeyBoard = new InlineKeyboardMarkup(new[]
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
            
            message = SendInlinePanel(messageEventArgs, inlineKeyBoard).Id;
            
        }

        protected override async Task SendInlinePanel(MessageEventArgs messageEventArgs, InlineKeyboardMarkup inlineKeyboardMarkup)
        {
            await Task.Run(() => BotLogic.Bot.SendPhotoAsync(messageEventArgs.Message.From.Id, "https://i.imgur.com/mbchaO0.png", "Меню:",
                replyMarkup: inlineKeyboardMarkup));
        }
    }
}