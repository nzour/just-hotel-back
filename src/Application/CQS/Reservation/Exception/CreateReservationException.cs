namespace Application.CQS.Reservation.Exception
{
    public class CreateReservationException : System.Exception
    {
        public CreateReservationException(string message): base(message)
        {
        }

        public static CreateReservationException DateToCantBeInPast()
        {
            return new CreateReservationException("Reservatoin date 'From' can't be in past.");
        }

        public static CreateReservationException InvalidDateValues()
        {
            return new CreateReservationException("Reservation date 'From' must me smaller than date 'To'.");
        }

        public static CreateReservationException DatesAreBusy()
        {
            return new CreateReservationException("Specified rent dates are busy.");
        }
    }
}
