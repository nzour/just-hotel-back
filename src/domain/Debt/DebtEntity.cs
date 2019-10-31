using System;
using Domain.DebtEntity;
using Domain.User;

#nullable disable

namespace Domain.Debt
{
    public class DebtEntity : AbstractEntity
    {
        public UserEntity Debtor { get; private set; }
        public Room.RoomEntity RoomEntity { get; private set; }
        public DateTime? HandedAt { get; private set; }
        public int Money { get; private set; }
        public bool IsHanded => null != HandedAt;

        protected DebtEntity()
        {
        }

        public DebtEntity(UserEntity debtor, Room.RoomEntity roomEntity, int? money = null)
        {
            Identify();

            if (UserRole.Client != Debtor.Role)
            {
                throw new DebtException($"Debtor must be with role '{UserRole.Client}', specified '{debtor.Role}'.");
            }

            Debtor = debtor;
            RoomEntity = roomEntity;
            Money = money ?? RoomEntity.Cost;
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