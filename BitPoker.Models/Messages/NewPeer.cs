using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Messages
{
    public class NewPeer : BaseRequest, IMessage
    {
        public PlayerInfo Player { get; set; }
    }
}
