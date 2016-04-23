using System;
using System.Collections.Generic;

namespace BitPoker.API.Models
{
    /// <summary>
    /// Internal model to persist game state
    /// </summary>
    public class Table
    {
        public List<Hand> Hands { get; set; }

        public Table()
        {
            this.Hands = new List<Hand>();
        }
    }
}