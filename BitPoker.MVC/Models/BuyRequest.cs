using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BitPoker.MVC.Models
{
    public class BuyRequest
    {
        [Required, Display(Name = "Asset ID")]
        public String AssetId { get; set; }

        [Required, Range(0, 100000)]
        public Int64 Amount { get; set; }

        public Decimal Rate { get; set; }

        [Display(Name = "Deliver Address")]
        public String DeliveryAddress { get; set; }

        [Display(Name = "Phone Number")]
        public Int64 PhoneNumber { get; set; }

        [EmailAddress]
        public String Email { get; set; }
    }
}
