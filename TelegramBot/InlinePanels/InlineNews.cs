using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InlinePanels
{
    public class InlineNews : InlineAbstractPanel
    {
        protected override async void EditInlinePanel(MessageEventArgs messageEventArgs, int messageId)
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
                    chatId: messageEventArgs.Message.From.Id,
                    messageId: messageId,
                    media: new InputMediaPhoto(new InputMedia(DataConnection.GetImage("News"), "News.png")),
                    replyMarkup: inlineKeyBoard);
                
                await BotController.Bot.EditMessageCaptionAsync(messageEventArgs.Message.From.Id, messageId, $"Самые свежие новости на сайте: ", replyMarkup: inlineKeyBoard);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                RunDefaultCreatingProcess(messageEventArgs);
            }
        }
    }
}