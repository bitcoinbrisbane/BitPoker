using System;

namespace BitPoker.Models.Messages
{
    public class BuyInRequest : BaseRequest
    {
        public Guid TableId { get; set; }

        //[Obsolete("Just use the tx")]
        //public UInt64 Amount { get; set; }

        public String TxID { get; set; }

        public BuyInRequest()
        {
            base.Version = 1.0M;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
