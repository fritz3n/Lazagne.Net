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
                    Console.WriteLine($"\tPassword: {login.Password}\n\n");
                }
            }

            Console.ReadLine();
        }
    }
}
