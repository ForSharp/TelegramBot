using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineReasons : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                       {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Низкие цены")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Бесплатная доставка")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Склад в Краснодаре")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Широчайший ассортимент")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Мы не срываем сроки")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Профессиональные сотрудники")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Гарантия качества")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Маркетинговая поддержка")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Назад"),
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });

            await BotController.Bot.EditMessageMediaAsync(
             chatId: userId,
             messageId: messageId,
             media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("Reasons"), "Reasons.png")),
             replyMarkup: inlineKeyBoard);
            
            await BotController.Bot.EditMessageCaptionAsync(userId, messageId, 
                "8 причин работать с нами:", replyMarkup: inlineKeyBoard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}