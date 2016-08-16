using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models;

namespace BitPoker.Repository
{
    public class MockHandRepo : IHandRepository
    {

        private PlayerInfo alice;
        private PlayerInfo bob;


        public MockHandRepo()
        {
            IPlayerRepository playerRepo = new MockPlayerRepo();
        }

        public void Add(Hand entity)
        {
        }

        public IEnumerable<Hand> All()
        {
            throw new NotImplementedException();
        }

        public Hand Find(Guid id)
        {
            switch (id.ToString())
            {
                case "398b5fe2-da27-4772-81ce-37fa615719b5":

                    Hand hand = new Hand()
                    {
                        Id = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5")
                    };

                    break;
                case "91dacf01-4c4b-4656-912b-2c3a11f6e516":
                    //heads up?
                    break;

            }

            throw new NotImplementedException();
        }

        public void Update(Hand entity)
        {
        }

        public Int32 Save()
        {
            return 0;
        }

        public void Dispose()
        {
        }
    }
}
