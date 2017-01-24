using System;
using System.Collections.Generic;
using System.Linq;
using BitPoker.Models;
using System.Runtime.Caching;

namespace BitPoker.MVC.Repository
{
    public class InMemoryPlayerRepo : BitPoker.Repository.IPlayerRepository
    {
        private const string KEY = "players";
        private CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();

        public IEnumerable<IPlayer> All()
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.PlayerContainer container = (Models.PlayerContainer)MemoryCache.Default[KEY];

                if (container != null)
                {
                    return container.Players;
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

        public IPlayer Find(string address)
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.PlayerContainer container = (Models.PlayerContainer)MemoryCache.Default[KEY];

                if (container != null)
                {
                    return container.Players.FirstOrDefault(p => p.IPAddress == address);
                }
                else
                {
                    throw new Exceptions.PlayerNotFoundException();
                }
            }
            else
            {
                return null;
            }
        }

        public void Add(IPlayer entity)
        {
            if (!MemoryCache.Default.Contains(KEY))
            {
                cacheItemPolicy.SlidingExpiration = new TimeSpan(1, 0, 0);
                Models.PlayerContainer container = new Models.PlayerContainer();
                container.Players.Add(entity);
                MemoryCache.Default.Add(KEY, container, cacheItemPolicy);
            }

            if (MemoryCache.Default.Contains(KEY))
            {
                Models.PlayerContainer container = (Models.PlayerContainer)MemoryCache.Default[KEY];

                if (container != null)
                {
                    container.Players.Add(entity);
                    MemoryCache.Default.Set(KEY, container, cacheItemPolicy);
                }
            }
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
