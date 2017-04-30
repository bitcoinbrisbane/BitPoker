using System;
using System.Collections.Generic;
using BitPoker.Models;

namespace BitPoker.Owin.RestHost.Repository
{
    public class InMemoryLogRepository : BitPoker.Repository.IAddAndReadRepository<Models.Log>
    {
		private readonly Int32 _maxRows;
        private List<Models.Log> _logs;

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
        }

        public IEnumerable<Log> All()
        {
            return _logs;
        }

        public Log Find(string id)
        {
            throw new NotImplementedException();
        }
    }
}