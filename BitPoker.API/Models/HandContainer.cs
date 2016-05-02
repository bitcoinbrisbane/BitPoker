using BitPoker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitPoker.API.Models
{
    public class HandContainer
    {
        public Guid Id { get; set; }

        public IList<Hand> Hands { get; set; }

        public HandContainer()
        {
            this.Hands = new List<Hand>();
        }
    }
}