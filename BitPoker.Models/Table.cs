using System;
using System.Collections.Generic;

namespace BitPoker.Models
{
    /// <summary>
    /// Internal model to persist game state
    /// </summary>
    public class Table
    {
        public Guid Id { get; set; }

        public List<Hand> Hands { get; set; }

        public Table()
        {
            this.Hands = new List<Hand>();
            this.Id = new Guid();
            this.Id = Guid.NewGuid();
        }

        public Table(Guid id)
        {
            this.Hands = new List<Hand>();
            this.Id = id;
        }
    }
}