using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Repository.SQLite
{
    public class Class1 : IPlayerRepository
    {
        public void Create()
        {

        }

        public void Add(PlayerInfo item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PlayerInfo> All()
        {
            throw new NotImplementedException();
        }

        public PlayerInfo Find(string address)
        {
            throw new NotImplementedException();
        }
    }
}
