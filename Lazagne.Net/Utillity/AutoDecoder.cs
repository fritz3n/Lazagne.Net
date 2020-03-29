using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Lazagne.Net.Utillity
{
    static class AutoDecoder
    {
        /// <summary>
        /// Automatically detects the text encoding of the data stored in buffer and decodes it.
        /// </summary>
        /// <param name="buffer">text data</param>
        /// <returns>decoded string from buffer or null if no suitable encoding was found</returns>
        public static string Decode(byte[] buffer)
        {
            using MemoryStream memory = new MemoryStream(buffer);

            Ude.CharsetDetector cdet = new Ude.CharsetDetector();
            cdet.Feed(memory);
            cdet.DataEnd();

            if (cdet.Charset == null)
                return null;

            try
            {
                Encoding encoding = Encoding.GetEncoding(cdet.Charset);

                return encoding.GetString(buffer);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
