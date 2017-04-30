using System;
using System.Threading.Tasks;

namespace BitPoker.Clients
{
	public interface IUserAgentClient
	{
		Task<Models.Messages.UserAgentResponse> GetUserAgentAsync(String networkAddress);
	}
}