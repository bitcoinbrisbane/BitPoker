using System;

namespace BitPoker.Models.Messages
{
    public class BuyInRequest : BaseRequest
    {
        public Guid TableId { get; set; }

        //[Obsolete("Just use the tx")]
        //public UInt64 Amount { get; set; }

        public String Tx { get; set; }

        public BuyInRequest()
        {
            base.Version = 1.0M;
        }

		public override string ToString()
		{
			return string.Format("[BuyInRequest: TableId={0}, Tx={1}]", TableId, Tx);
		}
    }
}
