using System;

namespace BitPoker.Models
{
	public class PostSmallBlindMessage : IMessage 
	{
		public String Tx { get; set; }

		public Int64 Amount { get;set; }

		public PostSmallBlindMessage ()
		{
		}
	}
}