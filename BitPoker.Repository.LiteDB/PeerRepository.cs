using System;
using System.Collections.Generic;
using BitPoker.Models;
using LiteDB;

namespace BitPoker.Repository.LiteDB
{
    public class PeerRepository : BaseRepository, IGenericRepository<Peer>
    {
        public PeerRepository(String filePath)
        {
            _filePath = filePath;
        }

        public void Add(Peer entity)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var peers = db.GetCollection<Peer>("peers");
                peers.Insert(entity);
            }
        }

        public IEnumerable<Peer> All()
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var peers = db.GetCollection<Peer>("peers");
                return peers.FindAll();
            }
        }

        public void Delete(Peer entity)
        {
            throw new NotImplementedException();
        }

        public Peer Find(String id)
        {
            throw new NotImplementedException();
        }

        public void Update(Peer entity)
        {
            throw new NotImplementedException();
        }
    }
}
