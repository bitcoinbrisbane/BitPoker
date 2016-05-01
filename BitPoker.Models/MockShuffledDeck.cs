using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    public class MockShuffledDeck : IDeck
    {
        private List<Byte[]> _cards;

        public IList<byte[]> Cards
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
            _cards = new List<byte[]>(52);

            Byte[] card = new Byte[] { 0x00 };
            _cards.Add(card);
        }

        public void Shuffle()
        {
        }
    }
}
