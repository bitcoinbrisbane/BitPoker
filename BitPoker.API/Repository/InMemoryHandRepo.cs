using System;
using System.Collections.Generic;
using System.Linq;
using BitPoker.Models;
using System.Runtime.Caching;

namespace BitPoker.API.Repository
{
    public class InMemoryHandRepo : BitPoker.Repository.IHandRepository
    {
        public IEnumerable<Hand> All()
        {
            //if (MemoryCache.Default.Contains(id.ToString()))
            //{
            //    Models.Hand hand = (Models.Hand)MemoryCache.Default[id.ToString()];
            //    if (id != null)
            //    {
            //        //return table.Hands.First(h => h.Id.ToString() == handId.ToString());
            //        return hand;
            //    }
            //    else
            //    {
            //        throw new Exceptions.HandNotFoundException();
            //    }
            //}
            //else
            //{
            //    throw new IndexOutOfRangeException();
            //}

            throw new NotImplementedException();
        }

        public Hand Find(Guid id)
        {
            if (MemoryCache.Default.Contains(id.ToString()))
            {
                Models.Hand hand = (Models.Hand)MemoryCache.Default[id.ToString()];
                if (id != null)
                {
                    //return table.Hands.First(h => h.Id.ToString() == handId.ToString());
                    return hand;
                }
                else
                {
                    throw new Exceptions.HandNotFoundException();
                }
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
