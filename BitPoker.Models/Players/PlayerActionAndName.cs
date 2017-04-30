using System;

namespace BitPoker.Models.Players
{
    public struct PlayerActionAndName
    {
        public PlayerActionAndName(String playerName, PlayerAction action)
        {
            this.PlayerName = playerName;
            this.Action = action;
        }

        public String PlayerName { get; }

        public PlayerAction Action { get; }
    }
}