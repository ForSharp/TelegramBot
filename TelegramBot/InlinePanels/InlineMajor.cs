using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineMajor : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Виды продукции")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Коротко о компании")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Причины работать с нами")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Расписание рейсов")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });
            
                await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Major"), "Major.png")), 
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                    $"Наша компания будет рада поработать с вами", replyMarkup: inlineKeyBoard);
                
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Major);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}