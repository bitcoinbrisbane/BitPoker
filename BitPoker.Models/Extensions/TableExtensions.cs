using System;

namespace BitPoker.Models.ExtensionMethods
{
    [Obsolete]
    public static class TableExtensions
    {
        public static Boolean IsValid(this Contracts.Table value)
        {
            //TODO:  MOVE TO HELPER
            //Some assertions
            if (value.SmallBlind > value.BigBlind)
            {
                return false;
            }

            if (value.MinBuyIn > value.MaxBuyIn)
            {
                return false;
            }

            return true;
        }
    }
}