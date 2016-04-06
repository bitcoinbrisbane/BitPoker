using System;

namespace BitPoker.Models.Messages
{
	public class ActionMessage
	{
        public String PublicKey { get; set; }

        public Guid HandId { get; set; }

        public Int32 Index { get; set; }

        public String Action { get; set; }

        public Int64 Amount { get; set; }

        public String Signature { get; set; }

        public String PreviousHash { get; set; }

		public ActionMessage ()
		{
		}

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}", PublicKey, HandId, Index, Action, Amount);
        }
	}
}