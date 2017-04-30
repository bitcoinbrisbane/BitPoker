using System;

namespace BitPoker.Security
{
	public interface IRandomProvider
	{
		Byte[] GetRandom(Int32 length);
	}
}
