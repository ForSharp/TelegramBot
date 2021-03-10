using System;
using System.Text;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramBot.InlinePanels;

namespace TelegramBot
{
    public static class MessageHandler
    {
        public static void HandleSenderMessage(string sender, MessageEventArgs messageEventArgs)
        {
            var msgType = messageEventArgs.Message.Type;
            
            switch (msgType)
            {
                case (MessageType.Text):
                    HandleMessageTypeText(sender, messageEventArgs);
                    break;
                case MessageType.Unknown:
                    HandleMessageTypeUnknown();
                    break;
                case MessageType.Photo:
                    HandleMessageTypePhoto();
                    break;
                case MessageType.Audio:
                    HandleMessageTypeAudio();
                    break;
                case MessageType.Video:
                    HandleMessageTypeVideo();
                    break;
                case MessageType.Voice:
                    HandleMessageTypeVoice();
                    break;
                case MessageType.Document:
                    HandleMessageTypeDocument();
                    break;
                case MessageType.Sticker:
                    HandleMessageTypeSticker();
                    break;
                case MessageType.Location:
                    HandleMessageTypeLocation();
                    break;
                case MessageType.Contact:
                    HandleMessageTypeContact();
                    break;
                case MessageType.Venue:
                    HandleMessageTypeVenue();
                    break;
                case MessageType.Game:
                    HandleMessageTypeGame();
                    break;
                case MessageType.VideoNote:
                    HandleMessageTypeVideoNote();
                    break;
                case MessageType.Invoice:
                    HandleMessageTypeTextInvoice();
                    break;
                case MessageType.SuccessfulPayment:
                    HandleMessageTypeSuccessfulPayment();
                    break;
                case MessageType.WebsiteConnected:
                    HandleMessageTypeWebsiteConnected();
                    break;
                case MessageType.ChatMembersAdded:
                    HandleMessageTypeChatMembersAdded();
                    break;
                case MessageType.ChatMemberLeft:
                    HandleMessageTypeChatMemberLeft();
                    break;
                case MessageType.ChatTitleChanged:
                    HandleMessageTypeChatTitleChanged();
                    break;
                case MessageType.ChatPhotoChanged:
                    HandleMessageTypeChatPhotoChanged();
                    break;
                case MessageType.MessagePinned:
                    HandleMessageTypeMessagePinned();
                    break;
                case MessageType.ChatPhotoDeleted:
                    HandleMessageTypeChatPhotoDeleted();
                    break;
                case MessageType.GroupCreated:
                    HandleMessageTypeGroupCreated();
                    break;
                case MessageType.SupergroupCreated:
                    HandleMessageTypeSupergroupCreated();
                    break;
                case MessageType.ChannelCreated:
                    HandleMessageTypeChannelCreated();
                    break;
                case MessageType.MigratedToSupergroup:
                    HandleMessageTypeMigratedToSupergroup();
                    break;
                case MessageType.MigratedFromGroup:
                    HandleMessageTypeMigratedFromGroup();
                    break;
                case MessageType.Animation:
                    HandleMessageTypeAnimation();
                    break;
                case MessageType.Poll:
                    HandleMessageTypePoll();
                    break;
                case MessageType.Dice:
                    HandleMessageTypeDice();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void HandleMessageTypeText(string sender, MessageEventArgs messageEventArgs)
        {
            
            
            Console.WriteLine($"{sender} отправил текстовое сообщение: {messageEventArgs.Message.Text}", true, Encoding.Unicode);
            
            switch (messageEventArgs.Message.Text.ToLower().Trim())
            {
                case "/start":
                    DataBaseContext.RegisterUser(messageEventArgs);
                    ProcessingTextMessages.CreateStartStatement(messageEventArgs);
                    break;
                case "меню":
                    var inlineMenu = new InlineMenu();
                    inlineMenu.RunCreatingProcess(messageEventArgs);
                    break;
                case "контакты":
                    ProcessingTextMessages.SendContacts();
                    break;
                case "заказать звонок":
                    ProcessingTextMessages.GetUserNumber();
                    break;
                case "показать на карте":
                    ProcessingTextMessages.ShowInTheMap();
                    break;
            }
            
            
        }

        

        private static void HandleMessageTypeUnknown()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypePhoto()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeAudio()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeVideo()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeVoice()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeDocument()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeSticker()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeLocation()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeContact()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeVenue()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeGame()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeVideoNote()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeTextInvoice()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeSuccessfulPayment()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeWebsiteConnected()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeChatMembersAdded()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeChatMemberLeft()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeChatTitleChanged()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeChatPhotoChanged()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeMessagePinned()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeChatPhotoDeleted()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeGroupCreated()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeSupergroupCreated()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeChannelCreated()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeMigratedToSupergroup()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeMigratedFromGroup()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeAnimation()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypePoll()
        {
            throw new NotImplementedException();
        }

        private static void HandleMessageTypeDice()
        {
            throw new NotImplementedException();
        }
    }
}