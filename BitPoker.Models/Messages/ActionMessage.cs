using System;
using System.Runtime.Serialization;

namespace BitPoker.Models.Messages
{
    [DataContract]
	public class ActionMessage : BaseMessage
	{
        public String PublicKey { get; set; }

        /// <summary>
        /// Include table id to make searching on hands more efficent
        /// </summary>
        public Guid TableId { get; set; }

        public Guid HandId { get; set; }

        public Int32 Index { get; set; }

        public String Action { get; set; }

        public UInt64 Amount { get; set; }

        public String Tx { get; set; }

        public String PreviousHash { get; set; }

        public String HashAlgorithm { get; set; }

        public ActionMessage ()
		{
            this.Id = Guid.NewGuid();
            this.TimeStamp = DateTime.UtcNow;
            this.HashAlgorithm = "SHA256";
            this.Version = 1.0M;
		}

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6:yyyyMMddHHmmss}{7}", Id, PublicKey, HandId, Index, Action, Amount, TimeStamp, Signature);
        }
	}
}