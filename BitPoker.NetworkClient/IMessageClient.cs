using System;
using System.Threading.Tasks;

namespace BitPoker.NetworkClient
{
	public interface IMessageClient
	{
        [Obsolete]
		void SendMessage(Models.Messages.ActionMessage message);

        [Obsolete]
        Task SendMessageAsync(Models.Messages.ActionMessage message);

        void SendIMessage(Models.IMessage message);
    }
}