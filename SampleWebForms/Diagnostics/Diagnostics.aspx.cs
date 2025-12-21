using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.ApplicationServices;

namespace SampleWebForms.Diagnostics
{
    public partial class Diagnostics : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //The things that change rarely, but could be upgraded
            //Also could ve different on each machine if this is a server farm.
            Computer c = new Computer();
            StringBuilder sb = new StringBuilder();
            sb.Append("Server Name: ");
            sb.Append(c.Name);
            sb.Append("<br/>");

            sb.Append("OS Full Name: ");
            sb.Append(c.Info.OSFullName);
            sb.Append("<br/>");

            sb.Append("Environment.MachineName : ");
            sb.Append(Environment.MachineName);
            sb.Append("<br/>");

            sb.Append("OS Platform: ");
            sb.Append(c.Info.OSPlatform);
            sb.Append("<br/>");

            sb.Append("OS Version: ");
            sb.Append(c.Info.OSVersion);
            sb.Append("<br/>");

            sb.Append("Environment.OSVersion : ");
            sb.Append(Environment.OSVersion );
            sb.Append("<br/>");

            sb.Append("Environment.ProcessorCount : ");
            sb.Append(Environment.ProcessorCount );
            sb.Append("<br/>");

            sb.Append("Installed Culture: ");
            sb.Append(c.Info.InstalledUICulture);
            sb.Append("<br/>");

            sb.Append("Avail Phys Memory: ");
            sb.Append(c.Info.AvailablePhysicalMemory);
            sb.Append("<br/>");

            //AssemblyInfo info = new Microsoft.VisualBasic.ApplicationServices.AssemblyInfo();
            sb.Append("Assembly Version: ");
            sb.Append(ProductVersion);
            sb.Append("<br/>");
 

            //If exists!
            MembershipUser u = Membership.GetUser();
            if(u!=null)
            {
                //Most attributes are very transitory, and related to password mgmt, e.g. locked out, password reset date, etc.
                sb.Append(u.ProviderUserKey);
                sb.Append("<br/>");
                sb.Append(u.CreationDate);
                sb.Append("<br/>");
            }

            //Can differ from thread principal!
            IPrincipal aspNetPrincipal = HttpContext.Current.User;
            
            IPrincipal principal = System.Threading.Thread.CurrentPrincipal;
            if (principal != null)
            {
                sb.Append(principal.Identity.Name); //Has names and role checks. Thats *all*
                sb.Append("<br/>");

                if(principal is WindowsPrincipal)
                {
                    WindowsPrincipal wp = (WindowsPrincipal) principal;//Has role checks
                    WindowsIdentity wi = (WindowsIdentity) wp.Identity;//Has groups, which *could* be useful if they reflect the organization
                    sb.Append("<br/>");
                }
            }


            Assembly current = Assembly.GetExecutingAssembly();
            //FileStream fs = current.GetFile(current.Location);
            FileInfo fi = new FileInfo(current.Location);

            sb.Append("Creation time of current assembly (proxy for version) " + fi.CreationTime);
            sb.Append("<br/>");
            sb.Append("File Size (another proxy for version) " + fi.Length);
            sb.Append("<br/>");

            //FileStream fs = fi.Open(FileMode.Open);
            //byte[] bytes = new byte[fs.Length];
            //fs.Read(bytes,0,(int)fs.Length);
            //int hash = ComputeHash(bytes);
            //sb.Append("Hash of executing assembly " +hash);

            sb.Append("<br/>");

            sb.Append("Executing assembly " + Assembly.GetExecutingAssembly().FullName);
            sb.Append("<br/>");
            sb.Append("<br/>");


            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach(Assembly assembly in assemblies)
            {
                string[] parts = assembly.FullName.Split(',');

                for (int i = 0; i < parts.Length;i++ )
                {
                    parts[i] = parts[i].Trim(new char[]{' '});
                }

                string[] foo = new string[] {"a", "B"};

                if (!UninterestingAssembly(parts[0]))
                    {
                        foreach (string part in parts)
                        {
                            if (part.StartsWith("Version"))
                            {
                                //Well, a user could set their version to 0, but normally this is dynamically compiled stuff
                                if (part.Split('=')[1].Trim() != "0.0.0.0")
                                {
                                    sb.Append("assembly " + parts[0] + " Version : ");
                                    sb.Append(part.Split('=')[1]);
                                    sb.Append("<br/>");
                                }
                            }
                        }
                    }

            }

            outputBox.Text = sb.ToString();

        }

        
        private static bool UninterestingAssembly(string part)
        {
            if (part == null) return true;
            //System stuff
            if(part.StartsWith("System"))return true;
            if (part.StartsWith("mscorlib")) return true;
            if (part.StartsWith("Microsoft.JScript")) return true;
            if (part.StartsWith("Microsoft.VisualBasic")) return true;
            if (part.StartsWith("VJSharpCodeProvider")) return true;
            if (part.StartsWith("CppCodeProvider")) return true;
            if (part.StartsWith("SMDiagnostics")) return true;
            
            
            //Dev server and dynamically compiled things
            if (part.StartsWith("App_Web_")) return true;
            if (part.StartsWith("WebDev.WebHost20")) return true;
            
            if (part.StartsWith("App_global.asax")) return true; 
            

            //Hicmah libraries and deps
            if (part.StartsWith("Hicmah")) return true;
            if (part.StartsWith("SharpDom")) return true;
            if (part.StartsWith("Newtonsoft.Json")) return true;
            if (part.StartsWith("NodaTime")) return true;
            if (part.StartsWith("protobuf-net")) return true;
            
            //Test web dependencies
            if (part.StartsWith("ErikEJ.SqlCeMembership")) return true;
            if (part.StartsWith("Elmah")) return true;
            

            return false;
        }

        //http://stackoverflow.com/questions/962639/detect-if-the-type-of-an-object-is-a-type-defined-by-net-framework
        static bool IsMicrosoftType(Type type)
        {
            object[] attrs = type.Assembly.GetCustomAttributes
                (typeof(AssemblyCompanyAttribute), false);

            return attrs.OfType<AssemblyCompanyAttribute>()
                        .Any(attr => attr.Company == "Microsoft Corporation");
        }

        public static int ComputeHash(params byte[] data)
        {
            unchecked
            {
                const int p = 16777619;
                int hash = (int)2166136261;

                for (int i = 0; i < data.Length; i++)
                    hash = (hash ^ data[i]) * p;

                hash += hash << 13;
                hash ^= hash >> 7;
                hash += hash << 3;
                hash ^= hash >> 17;
                hash += hash << 5;
                return hash;
            }
        }
        

        public static string ProductVersion
        {
            get
            {
                string productVersion=null;
                Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
                if (assembly != null)
                {
                    object[] customAttributes = assembly.GetCustomAttributes
                                    (typeof(AssemblyVersionAttribute), false);
                    if ((customAttributes != null) && (customAttributes.Length > 0))
                    {
                        productVersion =
                            ((AssemblyVersionAttribute)customAttributes[0]).Version;
                    }
                    if (string.IsNullOrEmpty(productVersion))
                    {
                        productVersion = string.Empty;
                    }
                }
                return productVersion;
            }
        }
    }
}
