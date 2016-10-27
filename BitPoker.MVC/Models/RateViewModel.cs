using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitPoker.MVC.Models
{
    public class RateViewModel : Rate
    {
        public Int64 AmountSold { get; set; }
    }
}