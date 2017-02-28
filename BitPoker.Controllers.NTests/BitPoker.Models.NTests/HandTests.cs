using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BitPoker.Models.NTests
{
	[TestFixture()]
	public class HandTests
	{
		private IPlayer[] mockPlayers;

		[SetUp()]
		public void Setup()
		{
			mockPlayers = new IPlayer[2];
			mockPlayers[0] = new TexasHoldemPlayer() { BitcoinAddress = "msPJhg9GPzMN6twknwmSQvrUKZbZnk51Tv" };
			mockPlayers[1] = new TexasHoldemPlayer() { BitcoinAddress = "mhSW3EUNoVkD1ZQV1ZpnxdRMBjo648enyo" };
		}

		[Test()]
		public void Should_Get_Dealer_As_Next_To_Act()
		{
			const Int16 expected = 0;

			Guid previousHandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5");
			Guid tableId = new Guid("e9571da4-372d-417b-98ab-e2b747880b86");

			Hand hand = new Hand(tableId, mockPlayers, null, previousHandId);
			Assert.AreEqual(expected, hand.PlayerToAct);
		}

		[Test()]
		public void Should_Get_Dealing_As_Allowed_Action()
		{
			List<String> expected = new List<String>(1);
			expected.Add("Dealing");

			Guid previousHandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5");
			Guid tableId = new Guid("e9571da4-372d-417b-98ab-e2b747880b86");

			Hand hand = new Hand(tableId, mockPlayers, null, previousHandId);
			Assert.AreEqual(expected, hand.AllowedActions);
			CollectionAssert.AreEqual(expected, hand.AllowedActions);
		}

		[Test()]
		public void Should_Get_Small_Blind_As_Next_To_Act()
		{
			const Int16 expected = 1;

			Guid previousHandId = new Guid("398b5fe2-da27-4772-81ce-37fa615719b5");
			Guid tableId = new Guid("e9571da4-372d-417b-98ab-e2b747880b86");

			Hand hand = new Hand(tableId, mockPlayers, null, previousHandId);

			hand.AddMessage(new BitPoker.Models.Messages.ActionMessage()
			{
				Action = "SMALLBLIND",
				Amount = 10000,
				HashAlgorithm = "SHA256"
			});

			Assert.AreEqual(expected, hand.PlayerToAct);
		}
	}
}