using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitPoker.Models.ExtensionMethods
{
    public static class ActionExtension
    {
        public static Boolean IsValid(this IEnumerable<Models.Messages.ActionMessage> value)
        {
            return false;
        }
    }
}
