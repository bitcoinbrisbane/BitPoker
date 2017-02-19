using System;
using System.Web.Http.Cors;

namespace BitPoker.Owin.RestHost
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class HandController : BitPoker.Controllers.Rest.HandController
	{
		public HandController()
		{
		}
	}
}