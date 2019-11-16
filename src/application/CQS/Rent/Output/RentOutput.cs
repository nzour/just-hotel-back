using System;
using System.Collections.Generic;
using System.Linq;
using Application.CQS.Room.Output;
using Application.CQS.Service.Output;
using Domain.Rent;

namespace Application.CQS.Rent.Output
{
    public class RentOutput
    {
        public Guid Id { get; }
        public DateTime StartedAt { get; }
        public DateTime ExpiredAt { get; }
        public uint Cost { get; }
        public RoomOutput Room { get; }
        public IEnumerable<ServiceOutput> Services { get; }

        public RentOutput(RentEntity rent)
        {
            Id = rent.Id;
            StartedAt = rent.StartedAt;
            ExpiredAt = rent.ExpiredAt;
            Cost = rent.Cost;
            Room = new RoomOutput(rent.Room);
            Services = rent.Services.Select(s => new ServiceOutput(s));
        }
    }
}
