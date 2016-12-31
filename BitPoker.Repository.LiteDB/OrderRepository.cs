using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Repository.LiteDB
{
    public class OrderRepository : BaseRepository, IRepository
    {
        public OrderRepository(String filePath)
        {
            _filePath = filePath;
        }

        public void Add(Order entity)
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
