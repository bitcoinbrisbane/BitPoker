using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.Helpers
{
    public class Chain
    {
        public Boolean IsValid(IEnumerable<Messages.ActionMessage> messages)
        {
            //
            foreach (Messages.ActionMessage action in messages.OrderBy(m => m.Index))
            {
                //Verify hash
                String hash = action.PreviousHash;

                //Verify signature
            }

            return true;
        }
    }
}
