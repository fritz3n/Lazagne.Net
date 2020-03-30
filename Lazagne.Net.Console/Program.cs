using System;
using System.Collections.Generic;

namespace Lazagne.Net.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<ApplicationInfo> credentials = ModuleHandler.GetCredentials();

            foreach (ApplicationInfo application in credentials)
            {
                Console.WriteLine(application);
            }

            Console.ReadLine();
        }
    }
}
