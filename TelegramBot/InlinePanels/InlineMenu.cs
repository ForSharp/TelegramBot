using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineMenu : InlineAbstractPanel
    {
        protected override async void CreateInlinePanel(int userId)
        {
            try
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

                var message = await BotController.Bot.SendPhotoAsync(userId,
                    DataConnection.GetImage("Menu"), "Меню:",
                    replyMarkup: inlineKeyboard);
            
                DataBaseContext.SetMessageId(userId, message.MessageId);
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Menu);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
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
            
                await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Menu"), "Menu.png")),
                    replyMarkup: inlineKeyboard);
            
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                    $"Меню:", replyMarkup: inlineKeyboard);
                
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.Menu);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}