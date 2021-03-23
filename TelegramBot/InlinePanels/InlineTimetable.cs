using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineTimetable : InlineAbstractPanel
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
                await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Timetable"), "Timetable.png")),
                    replyMarkup: inlineKeyBoard);

                string timetable = String.Join(null, DataBaseContext.GetTimetableTrips());

                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, $"Расписание: \n\n{timetable}", replyMarkup: inlineKeyBoard);
                
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Timetable);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
            
        }
    }
}