using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using System.Runtime.Caching;

namespace BitPoker.API.Repository
{
    public class InMemoryPlayerRepo : BitPoker.Repository.IPlayerRepository
    {
        private const string KEY = "players";

        public IEnumerable<PlayerInfo> All()
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

        public PlayerInfo Find(string address)
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.PlayerContainer container = (Models.PlayerContainer)MemoryCache.Default[KEY];

                if (container != null)
                {
                    return container.Players.FirstOrDefault(p => p.Address == address);
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

        public void Add(PlayerInfo item)
        {
            if (MemoryCache.Default.Contains(KEY))
            {
                Models.PlayerContainer container = (Models.PlayerContainer)MemoryCache.Default[KEY];

                if (container != null)
                {
                    container.Players.Add(item);
                }
            }
        }
    }
}
