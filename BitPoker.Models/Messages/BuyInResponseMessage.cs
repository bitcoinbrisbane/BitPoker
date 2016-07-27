using System;
using System.Collections;
using System.Collections.Generic;

namespace BitPoker.Models.Messages
{
    public class BuyInResponseMessage : BaseMessage
    {
        //public Contracts.Table Table { get; set; }
        public String TxID { get; set; }

        public BuyInResponseMessage()
        {
            base.Id = Guid.NewGuid();
        }
    }
}
