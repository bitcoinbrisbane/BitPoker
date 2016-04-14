using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitPoker.API.Models
{
    /// <summary>
    /// Internal model to persist game state
    /// </summary>
    internal class Table
    {
        internal List<Hand> Hands { get; set; }

        internal Table()
        {
            this.Hands = new List<Hand>();
        }
    }
}