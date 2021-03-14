using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReason6 : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });

                var message = await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reason6"), "Reason6.png")),
                    replyMarkup: inlineKeyBoard);
                
                var caption = await BotController.Bot.EditMessageCaptionAsync(userId, messageId,
                    "Наши менеджеры всегда помогут Вам выбрать необходимый товар, по самой низкой цене, " +
                    "а в случае необходимости, подобрать аналог с меньшей ценой или меньшим сроком поставки. " +
                    "Мы всегда на связи и готовы оправдать Ваше доверие.", 
                    replyMarkup: inlineKeyBoard);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}