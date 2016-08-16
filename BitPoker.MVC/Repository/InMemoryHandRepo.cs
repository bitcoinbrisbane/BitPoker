using System;
using System.Collections.Generic;
using System.Linq;
using BitPoker.Models;
using System.Runtime.Caching;

namespace BitPoker.MVC.Repository
{
    public class InMemoryHandRepo : BitPoker.Repository.IHandRepository
    {
        private const string KEY = "hands";
        private CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();

        public void Add(Hand entity)
        {
            cacheItemPolicy.SlidingExpiration = new TimeSpan(1, 0, 0);

            if (!MemoryCache.Default.Contains(KEY))
            {
                Models.HandContainer container = new Models.HandContainer();
                MemoryCache.Default.Add(KEY, container, cacheItemPolicy);
            }

            if (MemoryCache.Default.Contains(KEY))
            {
                Models.HandContainer container = (Models.HandContainer)MemoryCache.Default[KEY];

                if (container != null)
                {
                    container.Hands.Add(entity);
                    MemoryCache.Default.Set(KEY, container, cacheItemPolicy);
                }
            }
        }

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
                return null;
            }
        }

        public Hand Find(Guid id)
        {
            if (MemoryCache.Default.Contains(KEY))
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

        public void Update(Hand entity)
        {
            throw new NotImplementedException();
        }

        public Int32 Save()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
