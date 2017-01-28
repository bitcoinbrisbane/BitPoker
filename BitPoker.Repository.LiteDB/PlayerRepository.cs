using System;
using System.Collections.Generic;
using BitPoker.Models;
using LiteDB;

namespace BitPoker.Repository.LiteDB
{
    public class PlayerRepository<U> : BaseRepository, IPlayerRepository
    {
        public PlayerRepository(String filePath)
        {
            _filePath = filePath;
        }

        public void Add(IPlayer entity)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var players = db.GetCollection<TexasHoldemPlayer>("players");
                TexasHoldemPlayer player = entity as TexasHoldemPlayer;
                players.Insert(player);
            }
        }

        public IEnumerable<IPlayer> All()
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var players = db.GetCollection<TexasHoldemPlayer>("players");
                return players.FindAll();
            }
        }

        public IPlayer Find(string address)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var players = db.GetCollection<TexasHoldemPlayer>("players");
                return players.FindById(address);
            }
        }

        public int Save()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
