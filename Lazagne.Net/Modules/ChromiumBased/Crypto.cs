using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Lazagne.Net.Modules.ChromiumBased
{
    class Crypto
    {
        public static byte[] Unprotect(IList<byte> data) => ProtectedData.Unprotect(data.ToArray(), null, DataProtectionScope.CurrentUser);
        public static byte[] Decrypt(byte[] buffer, byte[] key)
        {

            //Grab Nonce
            byte[] nonce = new ArraySegment<byte>(buffer, 3, 12).ToArray();


            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, nonce.ToArray());
            cipher.Init(false, parameters);

            //Decrypt Cipher Text
            byte[] cipherText = new ArraySegment<byte>(buffer, 15, buffer.Length - 15).ToArray();
            byte[] plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

            int len = cipher.ProcessBytes(cipherText.ToArray(), 0, cipherText.Length, plainText, 0);
            cipher.DoFinal(plainText, len);



            return plainText;
        }
    }
}
