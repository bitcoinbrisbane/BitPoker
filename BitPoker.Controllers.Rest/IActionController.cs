using System;
using BitPoker.Repository;
using System.Collections.Generic;

namespace BitPoker.Controllers.Rest
{
	public interface IActionController
	{
		IHandRepository HandRepo { get; set; }

		IMessagesRepository MessageRepo { get; set; }

		IEnumerable<BitPoker.Models.Messages.ActionMessage> Get(string id);

		void Post(BitPoker.Models.Messages.ActionMessage message);
	}
}
