using System;

namespace BitPoker.MVC.Models
{
    public class Rate
    {
        public Int16 Id { get; set; }

        public Decimal Price { get; set; }

        public Int64 Max { get; set; }
    }
}