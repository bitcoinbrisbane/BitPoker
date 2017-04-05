using System;
namespace BitPoker.MVC.Models
{
	public class PseudoRandom : BitPoker.Models.IRandom
	{
		public PseudoRandom()
		{
		}

		public int Next()
		{
			throw new NotImplementedException();
		}
	}
}
