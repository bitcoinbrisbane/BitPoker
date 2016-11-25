using System;
using System.Collections.Generic;

namespace BitPoker.Models.Contracts
{
    /// <summary>
    /// Generic poker interface
    /// </summary>
    public interface IPokerContract
    {
        UInt64 SmallBlind { get; set; }

        UInt64 BigBlind { get; set; }

        Int16 MaxPlayers { get; set; }

        Int16 MinPlayers { get; set; }

        UInt64 MinBuyIn { get; set; }

        UInt64 MaxBuyIn { get; set; }

        Boolean ValidateChain(IEnumerable<Messages.ActionMessage> actions);

        //void Deal();
    }
}