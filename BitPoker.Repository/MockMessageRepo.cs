using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitPoker.Models.Messages;

namespace BitPoker.Repository
{
    public class MockMessageRepo : IMessagesRepository
    {

        public IEnumerable<ActionMessage> All()
        {

            throw new NotImplementedException();
        }

		public ActionMessage Find(String id)
		{
			////Get a fake message at that index
			//switch (index)
			//{
			//    case 0:
			//        //BOB 0.001 
			//        message = new Models.Messages.ActionMessage()
			//        {
			//            HandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"), //id
			//            Action = "POST SB",
			//            Amount = 100000,
			//            Index = 0,
			//            PublicKey = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo"
			//        };

			//        message.Signature = bob_secret.PrivateKey.SignMessage(message.ToString());
			//        return message;
			//    case 1:
			//        //ALICE 0.002 
			//        message = new Models.Messages.ActionMessage();
			//        break;
			//}
			throw new NotImplementedException();
		}

        public void Add(ActionMessage entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ActionMessage entity)
        {
        }

        public void Update(ActionMessage entity)
        {
        }

		public void Dispose()
		{
		}
    }
}
