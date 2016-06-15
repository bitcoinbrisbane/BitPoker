using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Mocks
{
    public class HeadsUpColdDeck : IDeck
    {
        public IList<byte[]> Cards
        {
            get;
            private set;
        }

        public bool IsEncrypted
        {
            get
            {
                return false;
            }
        }

        public HeadsUpColdDeck()
        {
            this.Cards = new List<Byte[]>(52);
        }

        public void Encrypt()
        {
        }

        public void New()
        {
        }

        public void Shuffle()
        {
        }
    }
}
