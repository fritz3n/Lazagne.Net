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
        public Dictionary<string, string> AdditionalData { get; set; }

        public LoginInfo(string url, string login, string password, Dictionary<string, string> additionalData = null)
        {
            Url = url;
            Login = login;
            Password = password;
            AdditionalData = additionalData;
        }
    }
}
