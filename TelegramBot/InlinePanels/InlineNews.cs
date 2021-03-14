using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineNews : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(int userId, int messageId)
        {
            try
            {
                string[] news = new string[5]
                {
                    "http://planeta-grupp.ru/news/postuplenie-novyh-modeley-svetilnikov-brixoll-0",
                    "http://planeta-grupp.ru/news/cena-na-kabel-eshche-nizhe",
                    "http://planeta-grupp.ru/news/my-stali-distribyutorami-kompanii-ekf",
                    "http://planeta-grupp.ru/news/postuplenie-absolyutnyh-novinok-svetilnikov-ambrella",
                    "http://planeta-grupp.ru/news/kabel-gost-cena-super"
                };
                Random rand = new Random();
                int i = rand.Next(news.Length);
                string n = news[i];

                var inlineKeyBoard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Новости", $"{n}"),
                        InlineKeyboardButton.WithCallbackData("Начало")
                    }
                });

                await BotController.Bot.EditMessageMediaAsync(
                    chatId: userId,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("News"), "News.png")),
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(userId, messageId, $"Самые свежие новости на сайте: ", replyMarkup: inlineKeyBoard);
                
                DataBaseContext.SetStepId(userId, (int)InlinePanelStep.News);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(userId);
            }
        }
    }
}