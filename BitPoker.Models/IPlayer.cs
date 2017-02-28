using System;

namespace BitPoker.Models
{
    public interface IPlayer
    {
        Int16 Position { get; set; }

        Boolean IsDealer { get; set; }

        Boolean IsSmallBlind { get; set; }

        Boolean IsBigBlind { get; set; }

        //Boolean IsTurnToAct { get; set; }

        String BitcoinAddress { get; set; }

        UInt64 Stack { get; set; }

        String IPAddress { get; set; }
    }
}
