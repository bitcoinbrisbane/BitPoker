using System;
using System.Threading.Tasks;

namespace BitPoker.NetworkClient
{
	public interface IMessageClient
	{
		void SendMessage(Models.Messages.ActionMessage message);

		Task SendMessageAsync(Models.Messages.ActionMessage message);
	}
}