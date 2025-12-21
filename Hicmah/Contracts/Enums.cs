using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Hicmah
{
    public enum RequestType
    {
        [Description("None")] 
        None = 0,
        [Description("Get")] 
        Get = 1,
        [Description("Post")] 
        Post = 2,
        [Description("Head")] 
        Head = 3,
        [Description("Put")] 
        Put = 4,
        [Description("Delete")] 
        Delete = 5,
        [Description("Trace")] 
        Trace = 6,
        [Description("Options")] 
        Options = 7,
        [Description("Connect")] 
        Connect = 8,
        [Description("Path")] 
        Path = 9
    }

    public enum Invoker
    {
        [Description("None")] 
        None = 0,
        [Description("Invoked by HTTP handler")] 
        HttpHandler = 1,
        [Description("Invoked by HTTP module")] 
        HttpModule = 2,
        [Description("Invoked in code by directly calling the library")] 
        CodeInvoke = 3,
        [Description("Invoked by asmx web service")] 
        AsmxInvoke = 4,
        [Description("Invoked by WCF web service")] 
        WcfInvoke = 5
    }
    
}
