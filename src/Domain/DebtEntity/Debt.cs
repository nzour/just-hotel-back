using System;
using App.Domain.RoomEntity;
using App.Domain.UserEntity;

#nullable disable

namespace App.Domain.DebtEntity
{
    public class Debt : AbstractEntity
    {
        public User Debtor { get; private set; }
        public Room Room { get; private set; }
        public DateTime? HandedAt { get; private set; }
        public int Money { get; private set; }
        public bool IsHanded => null != HandedAt;

        protected Debt()
        {
        }

        public Debt(User debtor, Room room, int? money = null)
        {
            if (UserRole.Client != Debtor.Role)
            {
                throw new DebtException($"Debtor must be with role '{UserRole.Client}', specified '{debtor.Role}'.");
            }

            Debtor = debtor;
            Room = room;
            Money = money ?? Room.Cost;
        }

        public void HandOver()
        {
            if (IsHanded)
            {
                throw new DebtException("Debt is already handed over.");
            }

            HandedAt = new DateTime();
        }
    }
}