using System;
using System.Collections.Generic;

namespace BitPoker.API.Models
{
    public class PlayerContainer
    {
        public IList<BitPoker.Models.PlayerInfo> Players { get; set; }

        public PlayerContainer()
        {
            this.Players = new List<BitPoker.Models.PlayerInfo>();
        }
    }
}
