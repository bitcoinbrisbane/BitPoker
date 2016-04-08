using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class HandViewModel
    {
        public ICollection<BitPoker.Models.Messages.ActionMessage> History { get; set; }
    }
}
