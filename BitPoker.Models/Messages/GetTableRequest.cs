using System;

namespace BitPoker.Models.Messages
{
    public class GetTableRequest : BaseRequest, IMessage
    {
        public String Recipient { get; set; }

        /// <summary>
        /// Buy in filter
        /// </summary>
        public Int64 MaxBuyIn { get; set; }

        /// <summary>
        /// Buy in filter
        /// </summary>
        public Int64 MinBuyIn { get; set; }
    }
}
