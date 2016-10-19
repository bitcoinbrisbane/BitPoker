using System;

namespace BitPoker.NetworkClient
{
	public interface INetworkClient : IDisposable
	{
		Boolean IsConnected { get; }
	}
}