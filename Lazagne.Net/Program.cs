using System;

namespace Lazagne.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (ApplicationInfo application in ModuleHandler.GetCredentials())
            {
                Console.WriteLine($"### {application.Name} ###");

                foreach (LoginInfo login in application.Logins)
                {
                    Console.WriteLine("\tPassword Found!!\n");

                    Console.WriteLine($"\tUrl: {login.Url}");
                    Console.WriteLine($"\tLogin: {login.Login}");
                    Console.WriteLine($"\tPassword: {login.Password}");

                    if ((login.AdditionalData?.Count ?? 0) > 0)
                    {
                        Console.WriteLine($"\tAdditionalData:");
                        foreach (System.Collections.Generic.KeyValuePair<string, string> data in login.AdditionalData)
                        {
                            Console.WriteLine($"\t\t{data.Key}: {data.Value}");
                        }
                    }
                    Console.WriteLine("\n");
                }
            }

            Console.ReadLine();
        }
    }
}
