using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;

namespace BitPoker.Controllers.Rest
{
	public class HandsController : BaseController, IHandController
	{
		public Int32 MaxRows { get; set; }

		public BitPoker.Repository.IHandRepository HandRepo { get; set; }

		public BitPoker.Repository.IMessagesRepository MessageRepo { get; set; }

		public HandsController()
		{
    		this.MaxRows = 1000;
			String key = System.Configuration.ConfigurationManager.AppSettings["BitcoinPrivateKey"];

			if (!String.IsNullOrEmpty(key))
			{
				base.PrivateKey = key;
			}
		}

		[HttpGet]
		public IEnumerable<BitPoker.Models.Hand> Get()
		{
			return HandRepo.All().Take(MaxRows);
		}

		[HttpGet]
		public IEnumerable<BitPoker.Models.Hand> Get(string id)
		{
			return HandRepo.All().Where(h => h.TableId.ToString() == id);
		}
    }
}