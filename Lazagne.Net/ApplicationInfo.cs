using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net
{
    public class ApplicationInfo
    {
        public string Name { get; set; }
        public ICollection<LoginInfo> Logins { get; set; }

        public ApplicationInfo(string name, ICollection<LoginInfo> logins)
        {
            Name = name;
            Logins = logins;
        }

        public static List<ApplicationInfo> Single(string name, List<LoginInfo> logins) => new List<ApplicationInfo>() { new ApplicationInfo(name, logins) };


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"### {Name} ###");
            foreach (var login in Logins)
            {
                sb.AppendLine(login.ToString("\t"));
            }

            return sb.ToString();
        }

    }
}
