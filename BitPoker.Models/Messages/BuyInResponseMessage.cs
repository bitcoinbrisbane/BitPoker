using System;
using System.Collections;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
    public class BuyInResponseMessage
    {
        //public PlayerInfo[] Players { get; set; }
        public Models.Contracts.Table Table { get; set; }

        public BuyInResponseMessage()
        {
            //this.Players = new PlayerInfo[n];
            this.Table = new Contracts.Table();
        }
    }
}
