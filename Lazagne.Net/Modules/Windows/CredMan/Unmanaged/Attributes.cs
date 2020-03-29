using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Lazagne.Net.Modules.Windows.CredMan.Unmanaged
{
    public enum CRED_TYPE : uint
    {
        GENERIC = 1,
        DOMAIN_PASSWORD = 2,
        DOMAIN_CERTIFICATE = 3,
        DOMAIN_VISIBLE_PASSWORD = 4,
        GENERIC_CERTIFICATE = 5,
        DOMAIN_EXTENDED = 6,
        MAXIMUM = 7,      // Maximum supported cred type
        MAXIMUM_EX = (MAXIMUM + 1000),  // Allow new applications to run on old OSes
    }

    public enum CRED_PERSIST : uint
    {
        SESSION = 1,
        LOCAL_MACHINE = 2,
        ENTERPRISE = 3,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct CREDENTIAL_ATTRIBUTE
    {
        string Keyword;
        uint Flags;
        uint ValueSize;
        IntPtr Value;
    }
}
