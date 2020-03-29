using System;
using System.Collections.Generic;
using System.IO;


namespace Lazagne.Net.Modules.Windows
{
    public class Wsl : IModule
    {
        private static string LocalAppdataPackages = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), "Packages");
        private static HashSet<string> InvalidHashes = new HashSet<string>() { "!","*", "x"};

        static Wsl()
        {
            ModuleHandler.Register(new Wsl());
        }

        public IEnumerable<ApplicationInfo> GetCredentials()
        {
            HashSet<ApplicationInfo> infos = new HashSet<ApplicationInfo>();
            foreach (string dir in Directory.GetDirectories(LocalAppdataPackages))
            {
                string shadowPath = Path.Combine(dir, "LocalState/rootfs/etc/shadow");
                if (File.Exists(shadowPath))
                {
                    ApplicationInfo currentInfo = new ApplicationInfo()
                    {
                        Name = Path.GetFileName(dir),
                        Logins = new HashSet<LoginInfo>()
                    };
                    
                    using (StreamReader sr = new StreamReader(new FileStream(shadowPath, FileMode.Open, FileAccess.Read)))
                    {
                        bool isAdded = false;
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            string[] split = line.Split(':');
                            if (!InvalidHashes.Contains(split[1]))
                            {
                                currentInfo.Logins.Add(new LoginInfo() 
                                { 
                                    Login = split[0],
                                    Password = split[1]
                                });

                                if(isAdded == false)
                                {
                                    infos.Add(currentInfo);
                                    isAdded = true;
                                }
                            }
                        }
                    }
                }
            }

            return infos;
        }
    }
}
