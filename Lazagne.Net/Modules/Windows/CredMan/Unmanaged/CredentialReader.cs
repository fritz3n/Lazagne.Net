using Lazagne.Net.Modules.Windows.CredMan.Unmanaged;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Lazagne.Net.Modules.Windows.CredMan
{
    class CredentialReader
    {
        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "CredEnumerateW")]
        static extern bool CredEnumerate(string filter, int flag, out int count, out IntPtr pCredentials);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool CredFree([In] IntPtr buffer);

        public static Credential[] GetCredentials()
        {
            Credential[] credentials = null;
            bool ret = CredEnumerate(null, 1, out int count, out IntPtr pCredentials);
            if (ret != false)
            {
                CredentialInMarshaler marshaller = new CredentialInMarshaler();

                credentials = new Credential[count];
                for (int n = 0; n < count; n++)
                {
                    IntPtr rawCred = Marshal.ReadIntPtr(pCredentials, n * Marshal.SizeOf(typeof(IntPtr)));

                    credentials[n] = (Credential)marshaller.MarshalNativeToManaged(rawCred);

                }

                CredFree(pCredentials);
            }
            else
            {
                throw new Exception("Credentials could not be read");
            }

            return credentials;
        }
    }
}
