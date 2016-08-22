using System;

namespace BitPoker.Models.ExtensionMethods
{
    public static class HandExtensions
    {
        public static PlayerInfo GetSmallBlind(this Hand value)
        {
            if (value.Players.Length > 0)
            {
                return value.Players[value.PlayerToAct];
            }
            else
            {
                return null;
            }
        }

        public static Boolean IsValid(this Hand value)
        {
            if (value.History != null)
            {

            }
            return false;
        }
    }
}
