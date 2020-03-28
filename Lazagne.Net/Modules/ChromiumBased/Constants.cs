using System;
using System.Collections.Generic;
using System.Text;

namespace Lazagne.Net.Modules.ChromiumBased
{
    class Constants
    {
        public static Browser[] Browsers { get; } = new Browser[]
        {
                new Browser(){Name = "7Star", Path = "{LOCALAPPDATA}\\7Star\\7Star\\User Data"},
                new Browser(){Name = "Amigo", Path = "{LOCALAPPDATA}\\Amigo\\User Data"},
                new Browser(){Name = "Brave", Path = "{LOCALAPPDATA}\\BraveSoftware\\Brave-Browser\\User Data"},
                new Browser(){Name = "Centbrowser", Path = "{LOCALAPPDATA}\\CentBrowser\\User Data"},
                new Browser(){Name = "Chedot", Path = "{LOCALAPPDATA}\\Chedot\\User Data"},
                new Browser(){Name = "Chrome Canary", Path = "{LOCALAPPDATA}\\Google\\Chrome SxS\\User Data"},
                new Browser(){Name = "Chromium", Path = "{LOCALAPPDATA}\\Chromium\\User Data"},
                new Browser(){Name = "Coccoc", Path = "{LOCALAPPDATA}\\CocCoc\\Browser\\User Data"},
                new Browser(){Name = "Comodo Dragon", Path = "{LOCALAPPDATA}\\Comodo\\Dragon\\User Data"},
                new Browser(){Name = "Elements Browser", Path = "{LOCALAPPDATA}\\Elements Browser\\User Data"},
                new Browser(){Name = "Epic Privacy Browser", Path = "{LOCALAPPDATA}\\Epic Privacy Browser\\User Data"},
                new Browser(){Name = "Google Chrome", Path = "{LOCALAPPDATA}\\Google\\Chrome\\User Data"},
                new Browser(){Name = "Kometa", Path = "{LOCALAPPDATA}\\Kometa\\User Data"},
                new Browser(){Name = "Opera", Path = "{APPDATA}\\Opera Software\\Opera Stable"},
                new Browser(){Name = "Orbitum", Path = "{LOCALAPPDATA}\\Orbitum\\User Data"},
                new Browser(){Name = "Sputnik", Path = "{LOCALAPPDATA}\\Sputnik\\Sputnik\\User Data"},
                new Browser(){Name = "Torch", Path = "{LOCALAPPDATA}\\Torch\\User Data"},
                new Browser(){Name = "Uran", Path = "{LOCALAPPDATA}\\uCozMedia\\Uran\\User Data"},
                new Browser(){Name = "Vivaldi", Path = "{LOCALAPPDATA}\\Vivaldi\\User Data"},
        };
    }
}
