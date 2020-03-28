using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Lazagne.Net
{
    static class ModuleHandler
    {
        private static List<IModule> modules = new List<IModule>();

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

        public static void Register(IModule module) => modules.Add(module);

        public static List<ApplicationInfo> GetCredentials()
        {
            List<ApplicationInfo> applications = new List<ApplicationInfo>();

            foreach (IModule module in modules)
                applications.AddRange(module.GetCredentials());

            return applications;
        }
    }
}
