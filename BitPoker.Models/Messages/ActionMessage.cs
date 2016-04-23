using System;

namespace BitPoker.Models.Messages
{
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

        public Int64 Amount { get; set; }

		public ActionMessage ()
		{
		}

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}{4}", PublicKey, HandId, Index, Action, Amount);
        }
	}
}