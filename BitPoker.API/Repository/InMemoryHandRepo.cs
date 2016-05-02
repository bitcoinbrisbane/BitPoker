using System;
using System.Collections.Generic;
using System.Linq;
using BitPoker.Models;
using System.Runtime.Caching;

namespace BitPoker.API.Repository
{
    public class InMemoryHandRepo : BitPoker.Repository.IHandRepository
    {
        private const string KEY = "hands";

        public IEnumerable<Hand> All()
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.HandContainer handContainer = (Models.HandContainer)MemoryCache.Default[KEY];

                if (handContainer != null)
                {
                    //return table.Hands.First(h => h.Id.ToString() == handId.ToString());
                    return handContainer.Hands;
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

        public Hand Find(Guid id)
        {
            if (MemoryCache.Default.Contains(KEY)
            {
                Models.HandContainer handContainer = (Models.HandContainer)MemoryCache.Default[KEY];

                if (handContainer != null)
                {
                    return handContainer.Hands.SingleOrDefault(h => h.Id.ToString() == id.ToString());
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
