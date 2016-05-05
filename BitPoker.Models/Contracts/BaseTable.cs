using System;

namespace BitPoker.Models.Contracts
{
    public abstract class BaseTable
    {
        public Guid Id { get; set; }

        public Int64 SmallBlind { get; set; }

        public Int64 BigBlind { get; set; }

        public Int16 MaxPlayers { get; set; }

        public Int16 MinPlayers { get; set; }
    }
}
