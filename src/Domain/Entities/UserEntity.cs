using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public enum UserRole
    {
        Manager,
        Employee,
        Client
    }

    public class UserEntity
    {
        public Guid Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; protected internal set; }
        public string Password { get; set; }
        public UserRole Role { get; protected internal set; }
        public ISet<ReservationEntity> Reservations { get; protected internal set; } = new HashSet<ReservationEntity>();

        public UserEntity(string firstName, string lastName, string login, string password, UserRole role)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
