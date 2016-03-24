using System;

namespace BitPoker.Models
{
	public interface IMoneyMessage
	{
		String Tx { get; set; }

		Int64 Amount { get;set; }
	}
}