using System;

namespace Lazagne.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (ApplicationInfo application in ModuleHandler.GetCredentials())
            {
                Console.WriteLine(application);
            }

            Console.ReadLine();
        }
    }
}
