using System;
using Domain.RoomEntity;
using Domain.UserEntity;

#nullable disable

namespace Domain.DebtEntity
{
    public class Debt : AbstractEntity
    {
        protected Debt()
        {
        }

        public Debt(User debtor, Room room, int? money = null)
        {
            Identify();

            if (UserRole.Client != Debtor.Role)
                throw new DebtException($"Debtor must be with role '{UserRole.Client}', specified '{debtor.Role}'.");

            Debtor = debtor;
            Room = room;
            Money = money ?? Room.Cost;
        }

        public User Debtor { get; private set; }
        public Room Room { get; private set; }
        public DateTime? HandedAt { get; private set; }
        public int Money { get; private set; }
        public bool IsHanded => null != HandedAt;

        public void HandOver()
        {
            if (IsHanded) throw new DebtException("Debt is already handed over.");

            HandedAt = new DateTime();
        }
    }
}