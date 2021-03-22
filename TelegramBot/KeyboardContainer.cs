using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class KeyboardContainer
    {
        public static ReplyKeyboardMarkup CreateDefaultKeyboard()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Меню"),
                    new KeyboardButton("Контакты")
                },
                new[]
                {
                    new KeyboardButton("Заказать звонок"),
                    new KeyboardButton("Показать на карте")
                }
            }, true, true);
            return replyKeyboard;
        }
        
        public static ReplyKeyboardMarkup CreateTwoKeyboardAdminButtons()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Назад"),
                    new KeyboardButton("Отмена")
                }
            }, true, true);

            return replyKeyboard;
        }
        
        public static ReplyKeyboardMarkup CreateThreeKeyboardAdminButtons()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Подтвердить")
                },
                new[]
                {
                    new KeyboardButton("Назад"),
                    new KeyboardButton("Отмена")
                }
            }, true, true);

            return replyKeyboard;
        }
        
        public static ReplyKeyboardMarkup CreateTimetableEditKeyboard()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Добавить рейс"),
                    new KeyboardButton("Редактировать рейс"),
                    new KeyboardButton("Удалить рейс")
                },
                new[]
                {
                    new KeyboardButton("Отмена")
                }
            }, true, true);
            return replyKeyboard;
        }
        
        public static ReplyKeyboardMarkup CreateTimetableEditTripKeyboard()
        {
            var replyKeyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton("Место отправки"),
                    new KeyboardButton("Место прибытия")
                },
                new[]
                {
                    new KeyboardButton("Дата отправки"),
                    new KeyboardButton("Дата прибытия")
                },
                new[]
                {
                    new KeyboardButton("Время отправки"),
                    new KeyboardButton("Время прибытия")
                },
                new[]
                {
                    new KeyboardButton("Назад"),
                    new KeyboardButton("Отмена")
                }
            }, true, true);

            return replyKeyboard;
        }
    }
}