using System;
using System.IO;

namespace Lazagne.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            string output = "";

            foreach (ApplicationInfo application in ModuleHandler.GetCredentials())
            {
                Console.WriteLine(application);
                output += application + "\n";
            }

            File.WriteAllText("output.txt", output);

            Console.ReadLine();
        }
    }
}
