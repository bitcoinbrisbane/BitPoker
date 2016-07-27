using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Repository
{
    public interface IRepository<T> //where T : IEntity
    {
        IEnumerable<T> All { get; }

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

        T FindById(int Id);
    }
}
