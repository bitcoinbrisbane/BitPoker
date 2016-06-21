using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models
{
    public interface IPlayer
    {
        Int16 Position { get; set; }

        Boolean IsDealer { get; set; }

        Boolean IsSmallBlind { get; set; }

        Boolean IsBigBlind { get; set; }

        Boolean IsTurnToAct { get; set; }

        String BitcoinAddress { get; set; }

        Int64 Stack { get; set; }
    }
}
