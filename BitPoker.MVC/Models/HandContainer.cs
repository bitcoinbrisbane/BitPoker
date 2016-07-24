using BitPoker.Models;
using System;
using System.Collections.Generic;

namespace BitPoker.MVC.Models
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