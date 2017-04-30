using System;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
	public interface ITransactionClient
	{
		Task<Models.Messages.UserAgentResponse> GetUnspentAsync(String address);
	}
}