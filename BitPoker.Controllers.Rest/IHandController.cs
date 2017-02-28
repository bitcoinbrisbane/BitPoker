using System;
namespace BitPoker.Controllers.Rest
{
	public interface IHandController
	{
		BitPoker.Repository.IHandRepository HandRepo { get; }

		BitPoker.Repository.IMessagesRepository MessageRepo { get; }
	}
}
