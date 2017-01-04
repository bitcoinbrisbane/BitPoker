using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models.Messages;

namespace BitPoker.Models.Contracts
{
    /// <summary>
    /// The responsibility of this contract is to 
    /// </summary>
    public class TexasHoldem : IPokerContract
    {
        public UInt64 SmallBlind { get; set; }

        public UInt64 BigBlind { get; set; }

        public Int16 MaxPlayers { get; set; }

        public Int16 MinPlayers { get; set; }

        public UInt64 MinBuyIn { get; set; }

        public UInt64 MaxBuyIn { get; set; }

        public bool ValidateChain(IEnumerable<ActionMessage> actions)
        {
            throw new NotImplementedException();
        }
    }
}
