using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net
{
    public class ApplicationInfo
    {
        public string Name { get; set; }
        public ICollection<LoginInfo> Logins { get; set; }

        public ApplicationInfo(string name, List<LoginInfo> logins)
        {
            Name = name;
            Logins = logins;
        }

        public static List<ApplicationInfo> Single(string name, List<LoginInfo> logins) => new List<ApplicationInfo>() { new ApplicationInfo(name, logins) };
    }
}
