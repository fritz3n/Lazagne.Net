using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lazagne.Net.Modules.ChromiumBased
{
    class Crypto
    {
        public static byte[] Unprotect(byte[] data) => ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
        public static byte[] Decrypt(byte[] buffer, byte[] key)
        {

            //Grab Nonce
            byte[] nonce = buffer[3..15];

            GcmBlockCipher cipher = new GcmBlockCipher(new AesEngine());
            AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, nonce);
            cipher.Init(false, parameters);

            //Decrypt Cipher Text
            byte[] cipherText = buffer[15..];
            byte[] plainText = new byte[cipher.GetOutputSize(cipherText.Length)];

            int len = cipher.ProcessBytes(cipherText, 0, cipherText.Length, plainText, 0);
            cipher.DoFinal(plainText, len);



            return plainText;
        }
    }
}
