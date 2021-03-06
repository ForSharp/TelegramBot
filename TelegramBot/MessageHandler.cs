using System;
using System.Linq;
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
                    HandleMessageTypeContact(messageEventArgs, messageEventArgs.Message.From.Id);
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
            var userId = messageEventArgs.Message.From.Id;
            var message = messageEventArgs.Message.Text;
            Console.WriteLine($"{sender} отправил текстовое сообщение: {message}", true, Encoding.Unicode);
            
            AdminCommandHandler.HandleAdminCommands(messageEventArgs);

            var keyCommands = new[]{"/admin", "/appoint", "/timetable", "/message" };
            
            if(DataBaseContextAdmin.GetCommandId(userId) != (int) AdminCommandStep.Default)
                return;
            
            switch (message.ToLower().Trim())
            {
                case "/start":
                    DataBaseContext.RegisterUser(messageEventArgs);
                    TextMessageProcessor.CreateStartStatement(messageEventArgs);
                    break;
                case "меню":
                    var inlineMenu = new InlineMenu();
                    inlineMenu.RunCreatingProcess(messageEventArgs, true);
                    break;
                case "контакты":
                    TextMessageProcessor.SendContacts(userId);
                    break;
                case "заказать звонок":
                    break;
                case "показать на карте":
                    TextMessageProcessor.ShowInTheMap(userId);
                    break;
                default:
                    if (!keyCommands.Contains(message))
                    {
                        TextMessageProcessor.SendAiAnswer(userId, message);
                    }
                    break;
            }
        }

        #region HandlingAnotherTypes

        private static void HandleMessageTypeUnknown()
        {
            
        }

        private static void HandleMessageTypePhoto()
        {
           
        }

        private static void HandleMessageTypeAudio()
        {
            
        }

        private static void HandleMessageTypeVideo()
        {
            
        }

        private static void HandleMessageTypeVoice()
        {
           
        }

        private static void HandleMessageTypeDocument()
        {
           
        }

        private static void HandleMessageTypeSticker()
        {
            
        }

        private static void HandleMessageTypeLocation()
        {
            
        }

        private static async void HandleMessageTypeContact(MessageEventArgs messageEventArgs, int userId)
        {
            
            try
            {
                var phoneNumber = messageEventArgs.Message.Contact.PhoneNumber;
                await BotController.Bot.SendTextMessageAsync(userId, "Мы свяжемся с Вами в ближайшее время.");
                DataBaseContext.SetPhoneNumber(userId, phoneNumber);

                var customer = BotController.GetAvailableSenderName(messageEventArgs);

                var admins = DataBaseContextAdmin.GetAllAdminId();
                foreach (var admin in admins)
                {
                    try
                    {
                        await BotController.Bot.SendTextMessageAsync(admin, $"{customer} запросил звонок, " +
                                                                            $"его номер: {DataBaseContext.GetPhoneNumber(userId)}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        private static void HandleMessageTypeVenue()
        {
            
        }

        private static void HandleMessageTypeGame()
        {
            
        }

        private static void HandleMessageTypeVideoNote()
        {
           
        }

        private static void HandleMessageTypeTextInvoice()
        {
            
        }

        private static void HandleMessageTypeSuccessfulPayment()
        {
            
        }

        private static void HandleMessageTypeWebsiteConnected()
        {
            
        }

        private static void HandleMessageTypeChatMembersAdded()
        {
            
        }

        private static void HandleMessageTypeChatMemberLeft()
        {
            
        }

        private static void HandleMessageTypeChatTitleChanged()
        {
            
        }

        private static void HandleMessageTypeChatPhotoChanged()
        {
            
        }

        private static void HandleMessageTypeMessagePinned()
        {
           
        }

        private static void HandleMessageTypeChatPhotoDeleted()
        {
           
        }

        private static void HandleMessageTypeGroupCreated()
        {
            
        }

        private static void HandleMessageTypeSupergroupCreated()
        {
            
        }

        private static void HandleMessageTypeChannelCreated()
        {
            
        }

        private static void HandleMessageTypeMigratedToSupergroup()
        {
            
        }

        private static void HandleMessageTypeMigratedFromGroup()
        {
            
        }

        private static void HandleMessageTypeAnimation()
        {
            
        }

        private static void HandleMessageTypePoll()
        {
            
        }

        private static void HandleMessageTypeDice()
        {
            
        }
        
        #endregion
    }
}