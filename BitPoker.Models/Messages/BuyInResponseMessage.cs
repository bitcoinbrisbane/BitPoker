using System;
using System.Collections;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
    public class BuyInResponseMessage
    {
        public Models.PlayerInfo[] Players { get; set; }

        public BuyInResponseMessage(Int32 n)
        {
            this.Players = new Models.PlayerInfo[n];
        }
    }
}
