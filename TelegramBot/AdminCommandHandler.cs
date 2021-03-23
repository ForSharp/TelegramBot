using Telegram.Bot.Args;

namespace TelegramBot
{
    public static class AdminCommandHandler
    {
        public static void HandleAdminCommands(MessageEventArgs messageEventArgs)
        {
            if (!DataBaseContextAdmin.CheckAdminRights(messageEventArgs.Message.From.Id))
                return;

            var userId = messageEventArgs.Message.From.Id;
            var message = messageEventArgs.Message.Text;
            var commandId = DataBaseContextAdmin.GetCommandId(userId);

            switch (commandId)
            {
                case (int) AdminCommandStep.Default:
                    AdminCommand.HandleDefaultCommands(userId, message);
                    break;
                case (int) AdminCommandStep.ShowUsers:
                    AdminCommand.HandleTargetName(userId, message);
                    break;
                case (int) AdminCommandStep.ConfirmUser:
                    AdminCommand.HandleConfirmingUser(messageEventArgs, userId, message);
                    break;
                case (int) AdminCommandStep.SendMessage:
                    AdminCommand.HandleSendMessage(messageEventArgs, userId, message);
                    break;
                case (int) AdminCommandStep.ConfirmSending:
                    AdminCommand.HandleConfirmingSending(userId, message);
                    break;
                case (int) AdminCommandStep.EditTimetable:
                    TimetableEditor.EditTimetable(userId, message);
                    break;
                case (int) AdminCommandStep.DeparturePlace:
                    TimetableEditor.SetDeparturePlace(userId, message);
                    break;
                case (int) AdminCommandStep.DepartureDate:
                    TimetableEditor.SetDepartureDate(userId, message);
                    break;
                case (int) AdminCommandStep.DepartureTime:
                    TimetableEditor.SetDepartureTime(userId, message);
                    break;
                case (int) AdminCommandStep.ArrivalPlace:
                    TimetableEditor.SetArrivalPlace(userId, message);
                    break;
                case (int) AdminCommandStep.ArrivalDate:
                    TimetableEditor.SetArrivalDate(userId, message);
                    break;
                case (int) AdminCommandStep.ArrivalTime:
                    TimetableEditor.SetArrivalTime(userId, message);
                    break;
                case (int) AdminCommandStep.SetTripIdEdit:
                    TimetableEditor.ChooseTripIdToEdit(userId, message);
                    break;
                case (int) AdminCommandStep.SetTripIdDel:
                    TimetableEditor.ChooseTripIdToDelete(userId, message);
                    break;
                case (int) AdminCommandStep.ChooseTripColumn:
                    TimetableEditor.ChooseTripColumn(userId, message);
                    break;
                case (int) AdminCommandStep.EditTripColumn:
                    TimetableEditor.EditTripColumn(userId, message);
                    break;
            }
        }
    }
}