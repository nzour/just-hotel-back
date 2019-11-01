using System;

namespace Domain.Rent
{
    public class RentException : Exception
    {
        public RentException(string message) : base(message)
        {
        }

        public static RentException InvalidDates()
        {
            return new RentException("Expired at date must be greater than started at date.");
        }
    }
}