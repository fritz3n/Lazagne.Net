using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net
{
    public class ApplicationInfo
    {
        public string Name { get; set; }
        public ICollection<LoginInfo> Logins { get; set; }
    }
}
