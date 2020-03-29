using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net
{
    public struct LoginInfo
    {
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IDictionary<string, string> AdditionalData { get; set; }

        public LoginInfo(string url, string login, string password, IDictionary<string, string> additionalData = null)
        {
            Url = url;
            Login = login;
            Password = password;
            AdditionalData = additionalData;
        }

        public override string ToString()
        {
            return ToString("");
        }

        public string ToString(string prepend, bool showAdditonal = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(prepend + "Url: ");
            sb.AppendLine(Url);

            sb.Append(prepend + "Login: ");
            sb.AppendLine(Login);

            sb.Append(prepend + "Password: ");
            sb.AppendLine(Password);

            if (AdditionalData != null && showAdditonal)
            {
                sb.AppendLine(prepend + "AdditionalData:");

                foreach (KeyValuePair<string, string> data in AdditionalData)
                {
                    sb.AppendLine(prepend + $"\t{data.Key}: {data.Value}");
                }
            }

            return sb.ToString();
        }
    }
}
