using System;
using System.Collections.Generic;
using System.Linq;
using BitPoker.Models;

namespace BitPoker.Owin.RestHost.Repository
{
    public class InMemoryLogRepository : BitPoker.Repository.IAddAndReadRepository<Log>
    {
		private readonly Int32 _maxRows;
        private List<Log> _logs;
        
		public InMemoryLogRepository(Int32 maxRows = 10000)
		{
			_maxRows = maxRows;
		}

        public void Add(Log entity)
        {
            _logs.Add(entity);

            if (_logs.Count > _maxRows)
            {
                _logs.Clear();
            }
            
            Console.WriteLine(entity);
        }

        public IEnumerable<Log> All()
        {
            return _logs;
        }

        public Log Find(string id)
        {
            return _logs.SingleOrDefault(l => l.Id.ToString() == id);
        }
    }
}