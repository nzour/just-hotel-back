namespace Application.CQS.Reservation.Exception
{
    public class ReservationException : System.Exception
    {
        public ReservationException(string message) : base(message)
        {
        }

        public static ReservationException DoesNotBelongToYou()
        {
            return new ReservationException($"Reservation doesn't belong to you.");
        }

        public static ReservationException AlreadyExpired()
        {
            return new ReservationException("Reservation rent date was expired. So it could not be deleted.");
        }
    }
}
