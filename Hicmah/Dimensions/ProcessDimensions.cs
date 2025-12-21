using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hicmah.Administration;
using Hicmah.Misc;
using Hicmah.Web;
using Wrappers.WebContext;

namespace Hicmah.Dimensions
{
    public class ProcessDimensions
    {
        private readonly TraceSourceUtil trace = new TraceSourceUtil("ProcessDimensions");
        private readonly ICacheWrapper cache;

        public ProcessDimensions()
        {
            if (HttpContext.Current==null)
                throw new InvalidOperationException("You need to call the constructor with the ICacheWrapper (maybe this is a unit or integration test?)");
            cache=new CacheWrapper();
        }

        //To be supplied by 
        public ProcessDimensions(ICacheWrapper cacheWrapper)
        {
            cache=cacheWrapper;
        }
        public void Execute(DatabaseAdministration admin)
        {
            trace.WriteLine("Processing all dimensions");

            using(ProcessUriDimension processor = new ProcessUriDimension())
            {
                processor.Process();
            }
            using (ProcessUserDimension processor = new ProcessUserDimension())
            {
                processor.Process();
            }

            ProcessDateDimension dateProcessor = new ProcessDateDimension(cache);
            dateProcessor.ProcessWithDateTime(admin);

            ProcessTimeDimension timeProcessor = new ProcessTimeDimension(cache);
            timeProcessor.Process();

            ProcessConstantDimensions constantProcessor = new ProcessConstantDimensions();
            constantProcessor.Process();
        }
    }
}
