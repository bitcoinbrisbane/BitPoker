using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models.Contracts;
using LiteDB;

namespace BitPoker.Repository.LiteDB
{
    public class TableRepository : BaseRepository, ITableRepository
    {
        public TableRepository(String filePath)
        {
            _filePath = filePath;
        }

        public void Add(Table entity)
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var tables = db.GetCollection<Table>("tables");
                tables.Insert(entity);
            }
        }

        public IEnumerable<Table> All()
        {
            using (var db = new LiteDatabase(_filePath))
            {
                var tables = db.GetCollection<Table>("tables");
                return tables.FindAll();
            }
        }

        public void Dispose()
        {
        }

        public Table Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            //return 0;
        }

        public void Update(Table entity)
        {
            throw new NotImplementedException();
        }
    }
}
