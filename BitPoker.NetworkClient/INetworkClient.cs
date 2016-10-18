using System;
namespace BitPoker.NetworkClient
{
	public interface INetworkClient
	{
		Boolean IsConnected { get; set; }
	}
}