using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net
{
    public interface IModule
    {
        IEnumerable<ApplicationInfo> GetCredentials();
    }
}
