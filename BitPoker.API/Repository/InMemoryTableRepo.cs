using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using BitPoker.Models.Contracts;

namespace BitPoker.API.Repository
{
    public class InMemoryTableRepo : BitPoker.Repository.ITableRepository
    {
        public IEnumerable<Table> All()
        {
            throw new NotImplementedException();
        }

        public Table Find(Guid id)
        {
            if (MemoryCache.Default.Contains(id.ToString()))
            {
                Table table = (Table)MemoryCache.Default[id.ToString()];
                return table;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}