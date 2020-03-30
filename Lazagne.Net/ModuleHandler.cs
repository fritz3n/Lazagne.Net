using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lazagne.Net
{
    public static class ModuleHandler
    {
        private static Dictionary<string, IModule> modules = new Dictionary<string, IModule>();

        static ModuleHandler()
        {
            Type[] types = Assembly.GetEntryAssembly().GetTypes();
            foreach (Type type in types)
            {
                if (typeof(IModule).IsAssignableFrom(type))
                {
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                }
            }
        }

        public static void Register(string name, IModule module) => modules.Add(name, module);

        public static IEnumerable<ApplicationInfo> GetCredentials()
        {
            List<ApplicationInfo> applications = new List<ApplicationInfo>();

            foreach (KeyValuePair<string, IModule> module in modules)
            {
                try
                {
                    applications.AddRange(module.Value.GetCredentials());
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error in module '{module.Key}':\n" + e);
                    continue;
                }
            }

            return applications;
        }
    }
}
