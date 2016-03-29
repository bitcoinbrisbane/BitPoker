using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class MainViewModel
    {
        private static Random rng = new Random();  

        public IObservable<BitPoker.Models.PlayerInfo> Players { get; set; }

        public BitPoker.Models.Contracts.Table Table { get; set; }

        public void NewDeck()
        {
            List<String> suites = new List<String>(4);
            suites.Add("H");
            suites.Add("D");
            suites.Add("S");
            suites.Add("C");

            IList<String> deck = new List<string>(52);

            foreach (String suite in suites)
            {
                for (Int32 i = 0; i < 13; i++)
                {
                    String card = String.Format("{0}{1}", i, suite);
                    deck.Add(card);
                }
            }

            //Shuffle
            IEnumerable<String> shuffledDeck = deck.Randomize();

            Int32 j = 0;
            foreach (String card in shuffledDeck)
            {
                Byte[] encryptedCard = EncryptStringToBytes_Aes(card, _keys[j], IV);
                Deck.Add(encryptedCard);

                Console.WriteLine(Convert.ToBase64String(encryptedCard));
                j++;
            }

            deck = null;
            shuffledDeck = null;
        }

        public void CreateKeys()
        {
            Byte[] allKeys = new Byte[832];

            for (Int32 i = 0; i < 52; i++)
            {
                Byte[] key = new Byte[16];
                rng.NextBytes(key);
                key.CopyTo(allKeys, i * 16);

                _keys.Add(key);
                Console.WriteLine(Convert.ToBase64String(key));
            }

            //Calculate hash on allKeys
        }

        public void Shuffle()
        {
            var deck = Deck.OrderBy(item => rng.Next());
        }

        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");

            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }
    }
}
