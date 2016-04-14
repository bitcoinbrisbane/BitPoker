using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Caching;

namespace BitPoker.API.Controllers
{
    public class TablesController : ApiController
    {
        public BitPoker.Models.Contracts.Table Get()
        {
            //Return a fake contract
            //{D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363}
            return new BitPoker.Models.Contracts.Table(2, 10) { Id =  new Guid("D6D9890D-0CA2-4B5D-AE98-FA4D45EB4363")};
        }

        public BitPoker.Models.Contracts.Table Get(Guid id)
        {
            //Return a fake contract, a heads up game with Alice and new user
            BitPoker.Models.Contracts.Table contract = new BitPoker.Models.Contracts.Table(2, 10) { Id = id };

            if (MemoryCache.Default.Contains(id.ToString()))
            {
                Models.Table table = (Models.Table)MemoryCache.Default[id.ToString()];

            }
            else
            {
                //Create new game
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.SlidingExpiration = new TimeSpan(0, 30, 0);

                Models.Table table = new Models.Table();
                MemoryCache.Default.Add(id.ToString(), table, policy);
            }
            
            return contract;
        }
    }
}