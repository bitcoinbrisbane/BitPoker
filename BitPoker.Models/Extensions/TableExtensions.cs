using System;
using System.Linq;
using System.Collections.Generic;

namespace BitPoker.Models.ExtensionMethods
{
    public static class TableExtensions
    {
		[Obsolete]
        public static Boolean IsValid(this Contracts.Table value)
        {
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

		public static String GetScriptAddress(this Contracts.Table value)
		{
			List<NBitcoin.PubKey> publicKeys = new List<NBitcoin.PubKey>();

			foreach (Models.Peer peer in value.Peers)
			{
				NBitcoin.PubKey pubKey = new NBitcoin.PubKey(peer.PublicKey);
				publicKeys.Add(pubKey);
			}

			NBitcoin.Script tableScript = NBitcoin.PayToMultiSigTemplate
				.Instance
				.GenerateScriptPubKey(value.MinPlayers, publicKeys.ToArray());

			return tableScript.GetScriptAddress(NBitcoin.Network.TestNet).ToString();
		}

		public static String GetScriptScript(this Contracts.Table value)
		{
			List<NBitcoin.PubKey> publicKeys = new List<NBitcoin.PubKey>();

			foreach (Models.Peer peer in value.Peers)
			{
				NBitcoin.PubKey pubKey = new NBitcoin.PubKey(peer.PublicKey);
				publicKeys.Add(pubKey);
			}

			NBitcoin.Script tableScript = NBitcoin.PayToMultiSigTemplate
				.Instance
				.GenerateScriptPubKey(value.MinPlayers, publicKeys.ToArray());

			return tableScript.ToString();
		}

		public static IEnumerable<String> GetPeersPublicKeys(this Contracts.Table value)
		{
			return value.Peers.Select(p => p.PublicKey);
		}
    }
}