using System;
using System.ComponentModel.DataAnnotations;

namespace BitPoker.MVC.Models
{
    public class Order
    {
        public Int32 Id { get; set; }

        public String RecipientAddress { get; set; }

        public Int64 Amount { get; set; }

        public Decimal BTCAmount { get; set; }

        public String PaymentAddress { get; set; }

        public String Tx { get; set; }
    }
}