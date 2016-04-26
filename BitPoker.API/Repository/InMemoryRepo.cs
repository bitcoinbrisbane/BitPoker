using System;
using System.Linq;
using System.Runtime.Caching;

namespace BitPoker.API.Repository
{
    public class InMemoryRepo : BitPoker.Repository.ITableRepository
    {
        public Models.Table Find(Guid id)
        {
            if (MemoryCache.Default.Contains(id.ToString()))
            {
                Models.Table table = (Models.Table)MemoryCache.Default[id.ToString()];
                return table;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public Models.Hand GetHand(Guid tableId, Guid handId)
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