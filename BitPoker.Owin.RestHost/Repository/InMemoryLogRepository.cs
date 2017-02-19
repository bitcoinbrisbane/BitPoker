using System;
using System.Collections.Generic;
using BitPoker.Models;

namespace BitPoker.Owin.RestHost.Repository
{
    public class InMemoryLogRepository : BitPoker.Repository.IAddAndReadRepository<Models.Log>
    {
        private List<Models.Log> _logs;

        public void Add(Log entity)
        {
            _logs.Add(entity);

            if (_logs.Count > 10000)
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
