using System;
using System.Web.Http;

namespace BitPoker.Owin.RestHost
{
	public class TestController : ApiController
	{
		[HttpGet]
		public String Get()
		{
			return "meow";
		}
	}
}
