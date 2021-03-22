namespace TelegramBot
{
    public enum AdminCommandStep
    {
        Default = 0,
        ShowUsers = 100,
        ConfirmUser = 101,
        SendMessage = 200,
        ConfirmSending = 201,
        DeparturePlace = 300,
        DepartureDate = 301,
        DepartureTime = 302,
        ArrivalPlace = 303,
        ArrivalDate = 304,
        ArrivalTime = 305,
        EditTimetable = 400,
        ChooseTripColumn = 500,
        EditTripColumn = 600,
        SetTripColumnEdit = 700,
        SetTripColumnDel = 701,
        DeleteTrip = 800
    }
}