using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Http;

namespace BitPoker.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        public Boolean Verify(String address, String signature)
        {
            return false;
        }

        public Models.Table GetTableFromCache(Guid tableId)
        {
            if (MemoryCache.Default.Contains(tableId.ToString()))
            {
                Models.Table table = (Models.Table)MemoryCache.Default[tableId.ToString()];
                return table;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public Models.Hand GetHandFromCache(Guid tableId, Guid handId)
        {
            if (MemoryCache.Default.Contains(tableId.ToString()))
            {
                Models.Table table = (Models.Table)MemoryCache.Default[tableId.ToString()];
                if (table != null)
                {
                    return table.Hands.First(h => h.Id.ToString() == handId.ToString());
                }
                else
                {
                    throw new Exceptions.HandNotFoundException();
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
