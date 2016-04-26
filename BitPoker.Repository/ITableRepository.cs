using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Repository
{
    public interface ITableRepository
    {
        Models.Table Find(Guid id);
    }
}
