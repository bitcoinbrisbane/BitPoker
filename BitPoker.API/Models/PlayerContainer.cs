using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.API.Models
{
    public class PlayerContainer
    {
        public IList<BitPoker.Models.PlayerInfo> Players { get; set; }
    }
}
