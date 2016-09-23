using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;
using LiteDB;

namespace BitPoker.Repository.LiteDB
{
    public class HandRepository : IHandRepository, IRepository
    {
        private readonly String _filePath;

        public HandRepository(String filePath)
        {
            _filePath = filePath;
        }

        public void Add(Hand entity)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var hands = db.GetCollection<Hand>("hands");
                hands.Insert(entity);
            }
        }

        public IEnumerable<Hand> All()
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var hands = db.GetCollection<Hand>("hands");
                //return hands;
                throw new NotImplementedException();
            }
        }

        public Hand Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            return 0;
        }

        public void Update(Hand entity)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var hands = db.GetCollection<Hand>("hands");
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
        }
    }
}
