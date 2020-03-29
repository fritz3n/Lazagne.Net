using Lazagne.Net.Modules.Windows.CredMan.Unmanaged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net.Modules.Windows.CredMan
{
    class CredMan : IModule
    {
        static CredMan()
        {
            ModuleHandler.Register("Windows Credential Manager", new CredMan());
        }

        public IEnumerable<ApplicationInfo> GetCredentials()
        {
            Credential[] credentials = CredentialReader.GetCredentials();

            List<LoginInfo> logins = new List<LoginInfo>();

            foreach (Credential credential in credentials)
            {
                if (credential.CredentialBlob.Length == 0)
                    continue;

                string password = Encoding.UTF8.GetString(credential.CredentialBlob);
                logins.Add(new LoginInfo(credential.TargetName, credential.UserName, password));
            }

            return ApplicationInfo.Single("Windows Credential Manager", logins);
        }
    }
}
