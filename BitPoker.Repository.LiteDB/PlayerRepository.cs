using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using LiteDB;

namespace BitPoker.Repository.LiteDB
{
    public class PlayerRepository : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(String filePath)
        {
            _filePath = filePath;
        }

        public void Add(PlayerInfo entity)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var players = db.GetCollection<PlayerInfo>("players");
                players.Insert(entity);
            }
        }

        public IEnumerable<PlayerInfo> All()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public PlayerInfo Find(string address)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return 0;
        }
    }
}
