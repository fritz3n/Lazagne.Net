using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lazagne.Net.Modules.Windows.CredMan.Unmanaged
{
    public class Credential
    {
        public UInt32 Flags;
        public CRED_TYPE Type;
        public string TargetName;
        public string Comment;
        public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        public byte[] CredentialBlob;
        public CRED_PERSIST Persist;
        public CREDENTIAL_ATTRIBUTE[] Attributes;
        public string TargetAlias;
        public string UserName;
    }

    /// <summary>
    ///
    /// </summary>
    public class CredentialInMarshaler : ICustomMarshaler
    {

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool CredFree([In] IntPtr buffer);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private class NATIVECREDENTIAL
        {
            public UInt32 Flags;
            public CRED_TYPE Type;
            public string TargetName;
            public string Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public UInt32 CredentialBlobSize;
            public IntPtr CredentialBlob;
            public CRED_PERSIST Persist;
            public UInt32 AttributeCount;
            public IntPtr Attributes;
            public string TargetAlias;
            public string UserName;
        }

        public void CleanUpManagedData(object ManagedObj)
        {
            // Nothing to do since all data can be garbage collected.
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
            {
                return;
            }
            CredFree(pNativeData);
        }

        public int GetNativeDataSize() => throw new NotImplementedException();

        public IntPtr MarshalManagedToNative(object obj) => throw new NotImplementedException();

        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
            {
                return null;
            }

            NATIVECREDENTIAL lRawCredential = (NATIVECREDENTIAL)Marshal.PtrToStructure(pNativeData, typeof(NATIVECREDENTIAL));

            Credential lCredential = new Credential()
            {
                UserName = lRawCredential.UserName,
                TargetName = lRawCredential.TargetName,
                TargetAlias = lRawCredential.TargetAlias,
                Persist = lRawCredential.Persist,
                Comment = lRawCredential.Comment,
                Flags = lRawCredential.Flags,
                LastWritten = lRawCredential.LastWritten,
                Type = lRawCredential.Type,
                CredentialBlob = new byte[lRawCredential.CredentialBlobSize],
                Attributes = new CREDENTIAL_ATTRIBUTE[lRawCredential.AttributeCount]
            };
            if (lRawCredential.CredentialBlobSize > 0)
                Marshal.Copy(lRawCredential.CredentialBlob, lCredential.CredentialBlob, 0, (int)lRawCredential.CredentialBlobSize);

            return lCredential;
        }

        public static ICustomMarshaler GetInstance(string cookie) => new CredentialInMarshaler();
    }
}
