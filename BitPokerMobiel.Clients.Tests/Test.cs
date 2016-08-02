using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace BitPokerMobile.Clients.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public async Task TestCase()
		{
			using (Clients.IPlayerClient client = new Clients.BitPokerRestClient())
			{
				var players = await client.GetPlayersAsync();
				Assert.IsNotNull(players);
			}
		}
	}
}