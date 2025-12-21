using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hicmah.Administration
{
    public class Ts
    {
        private string prefix = "{$ns}_";

        public Ts()
        {
            //TODO: Get prefix from config.
        }

        public string Hits
        {
            get { return prefix + "hits"; }
        }

    }
}
