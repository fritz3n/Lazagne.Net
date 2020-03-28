using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net
{
    interface IModule
    {
        IEnumerable<ApplicationInfo> GetCredentials();
    }
}
