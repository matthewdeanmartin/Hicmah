using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace Hicmah.Misc
{
    //This is so much easier than ilmerge
    public static class AssemblyLoading
    {
        public static void RegisterAssemblyResolver()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string toLoad = args.Name;
                if (args.Name.Contains(","))
                {
                    toLoad = toLoad.Split(',')[0];
                }

                //Mark file as resource (build properties) and then compile, find name
                String resourceName = "Resource." + new AssemblyName(toLoad).Name + ".dll";


                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (stream == null || stream.Length == 0)
                    {
                        //string.Join(" --- ", Assembly.GetExecutingAssembly().GetManifestResourceNames())
                        return null;
                    }
#if RELEASE
                    try
                    {
#endif
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
#if RELEASE
                    }
                    catch (Exception)
                    {
                        return null;
                    }
#endif
                }
            };
        }
    }
}

