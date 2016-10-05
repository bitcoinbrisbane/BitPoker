using System;

namespace BitPoker.Models.Messages
{
    public class BuyInRequestMessage : BaseMessage
    {
        public Guid TableId { get; set; }

        public UInt64 Amount { get; set; }

        public Int16 Seat { get; set; }

        /// <summary>
        /// Pub key required for multisig.  
        /// </summary>
        public String PublicKey { get; set; }

        public BuyInRequestMessage()
        {
            this.Version = 1;
            this.Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6:yyyyMMddHHmmss}{7}", BitcoinAddress, TableId, Amount, Seat, TimeStamp, Signature);
        }
    }
}
