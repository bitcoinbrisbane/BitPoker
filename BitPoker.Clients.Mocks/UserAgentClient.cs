using System;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Clients.Mocks
{
	public class UserAgentClient : IUserAgentClient
	{
		public async Task<Models.Messages.UserAgentResponse> GetUserAgentAsync(string networkAddress)
		{
			Models.Messages.UserAgentResponse response = new Models.Messages.UserAgentResponse();
			return await Task.FromResult<Models.Messages.UserAgentResponse>(response);
		}
	}
}