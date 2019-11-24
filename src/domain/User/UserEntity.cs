using System.Collections.Generic;
using Domain.Reservation;
using Domain.Transaction;

namespace Domain.User
{
    public class UserEntity : AbstractEntity
    {
        public string FirstName { get; protected internal set; }
        public string LastName { get; protected internal set; }
        public string Login { get; protected internal set; }
        public string Password { get; protected internal set; }
        public UserRole Role { get; protected internal set; }
        public ISet<TransactionEntity> Transactions { get; protected internal set; } = new HashSet<TransactionEntity>();
        public ISet<ReservationEntity> Reservations { get; protected internal set; } = new HashSet<ReservationEntity>();

        public UserEntity(string firstName, string lastName, string login, string password, UserRole role)
        {
            Identify();
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Role = role;
        }
    }

    public enum UserRole
    {
        Manager,
        Employee,
        Client
    }
}
