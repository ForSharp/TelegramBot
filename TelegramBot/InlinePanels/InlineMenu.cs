using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineMenu : InlineAbstractPanel
    {
        protected override async void CreateInlinePanel(MessageEventArgs messageEventArgs)
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

                var message = await BotController.Bot.SendPhotoAsync(messageEventArgs.Message.From.Id,
                    DataConnection.GetImage("Menu"), "Меню:",
                    replyMarkup: inlineKeyboard);
            
                DataBaseContext.SaveMessageId(messageEventArgs, message.MessageId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
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
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Menu"), "Menu.png")),
                    replyMarkup: inlineKeyboard);
            
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, 
                    $"Меню:", replyMarkup: inlineKeyboard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}