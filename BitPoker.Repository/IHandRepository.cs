using System;
using System.Collections.Generic;

namespace BitPoker.Repository
{
    public interface IHandRepository
    {
        IEnumerable<Models.Hand> All();

        Models.Hand Find(Guid id);
    }
}
