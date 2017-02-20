using System;

namespace BitPoker.Models
{
    public abstract class BaseTable
    {
        public Guid Id { get; set; }

        public UInt64 SmallBlind { get; set; }

        public UInt64 BigBlind { get; set; }

        public Int16 MaxPlayers { get; set; }

        public Int16 MinPlayers { get; set; }

        public UInt64 MinBuyIn { get; set; }

        public UInt64 MaxBuyIn { get; set; }

		public String HashAlgorithm { get; set; }

        public String NetworkAddress { get; set; }

        //last seen?
    }
}
