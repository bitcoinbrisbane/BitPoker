using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models
{
    public class MockShuffledDeck : IDeck
    {
        private ICollection<Byte[]> _cards;

        public ICollection<byte[]> Cards
        {
            get
            {
                return _cards;
            }
        }

        public bool IsEncrypted
        {
            get { return true; }
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
