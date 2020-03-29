using Lazagne.Net.Modules.Windows.CredMan.Unmanaged;
using Lazagne.Net.Utillity;
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
                switch (credential.Type)
                {
                    case CRED_TYPE.DOMAIN_PASSWORD:
                    case CRED_TYPE.DOMAIN_VISIBLE_PASSWORD:
                    case CRED_TYPE.GENERIC:
                        string password = AutoDecoder.Decode(credential.CredentialBlob);
                        logins.Add(new LoginInfo(credential.TargetName, credential.UserName, password));
                        break;
                    default:
                        string encPassword = BitConverter.ToString(credential.CredentialBlob); ;
                        logins.Add(new LoginInfo(credential.TargetName, credential.UserName, encPassword, new Dictionary<string, string>() {{"Type","" + (int)credential.Type }}));
                        break;

                }
                
            }

            return ApplicationInfo.Single("Windows Credential Manager", logins);
        }
    }
}
