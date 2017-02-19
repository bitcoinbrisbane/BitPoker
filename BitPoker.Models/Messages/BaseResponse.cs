using System;
namespace BitPoker.Models
{
	public abstract class BaseResponse
	{
		public Guid Id { get; set; }

		public DateTime TimeStamp { get; set; }

		public BaseResponse()
		{
		}
	}
}
