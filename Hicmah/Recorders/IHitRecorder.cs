using System;
using System.Collections.Generic;
using System.Text;

namespace Hicmah
{
    
    
    /// <summary>
    /// This is the class that processes the information about a hit excluding the recording to specific database.
    /// It is to be called by the handler, module, and any other invocation method
    /// It is also responsible for any logic 
    /// </summary>
    public interface IHitRecorder : IDisposable
    {
        /// <summary>
        /// This may write immediately or cache
        /// </summary>
        /// <param name="hit"></param>
        void InsertHit(Hit hit);

        /// <summary>
        /// This will write any cached hits to disk.
        /// </summary>
        void Flush();

        void UseAsync();

        int Capacity { get; set; }
    }

    public interface IHitRecorderCacheable : IHitRecorder
    {
        //TODO: factor out the top stuff.
    }
}
