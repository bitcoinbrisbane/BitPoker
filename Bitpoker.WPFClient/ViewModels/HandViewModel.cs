using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bitpoker.WPFClient.ViewModels
{
    public class HandViewModel
    {
        private static Random rng = new Random();

        internal ICollection<Byte[]> Keys { get; set; }

        internal Byte[] IV { get; set; }

        public ICollection<BitPoker.Models.Messages.ActionMessage> History { get; set; }

        public void CreateKeys()
        {
            Byte[] allKeys = new Byte[832];

            for (Int32 i = 0; i < 52; i++)
            {
                Byte[] key = new Byte[16];
                rng.NextBytes(key);
                key.CopyTo(allKeys, i * 16);

                this.Keys.Add(key);
            }
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
