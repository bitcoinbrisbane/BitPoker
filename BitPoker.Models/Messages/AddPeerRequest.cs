using System;

namespace BitPoker.Models.Messages
{
    public class AddPeerRequest : BaseRequest, IMessage
    {
        public String NetworkAddress { get; set; }

		//public String UserAgent { get; set; }

		///// <summary>
		///// ID of peer
		///// </summary>
		//public String PeersBitcoinAddress { get; set; }

		//public String PublicKey { get; set; }

        //public AddPeerRequest()
        //{
        //    base.Version = 1.0M;
        //}
    }
}