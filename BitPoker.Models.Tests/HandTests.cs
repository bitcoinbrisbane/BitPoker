using System;
using System.Linq;
using BitPoker.Models.ExtensionMethods;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

namespace BitPoker.Models.Tests
{
    [TestFixture]
    public class HandTests
    {
        private Hand headsUpHand;

        [SetUp]
        public void Setup()
        {
            Repository.IHandRepository mockHandRepo = new Repository.Mocks.HandRepository();
            Repository.IPlayerRepository mockPlayers = new Repository.Mocks.PlayerRepository();

            var aliceAndBob = mockPlayers.All();
            headsUpHand = mockHandRepo.Find(new Guid("398b5fe2-da27-4772-81ce-37fa615719b5"));
        }

        [Test]
        public void Should_Get_Hand_ToString()
        {
            String actual = headsUpHand.ToString();
            Assert.AreEqual("2d59577d-0f42-4a11-ae14-78ccf5b4b2e000000000-0000-0000-0000-0000000000000", actual);
        }

        [Test]
        public void Should_Get_History_As_XML()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(List<Messages.ActionMessage>));
            using (StringWriter sww = new StringWriter())
            {
            }
            //using (XmlWriter writer = XmlWriter.Create(sww))
            //{
            //    xsSubmit.Serialize(writer, headsUpHand.History);
            //    String xml = sww.ToString();
            //    Assert.IsNotNull(xml);
            //}
        }
    }
}
