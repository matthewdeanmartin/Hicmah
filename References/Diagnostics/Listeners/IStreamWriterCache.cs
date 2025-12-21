using System;
namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// A cache for stream writers
    /// </summary>
    public interface IStreamWriterCache
    {
        /// <summary>
        /// Clear out any streams not used since the date specified
        /// </summary>
        /// <param name="notUsedSince">Clear all streams that have not been used since this timstamp</param>
        void ClearOldStreams(DateTime notUsedSince);

        /// <summary>
        /// Return a stream writer for the passed path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        System.IO.StreamWriter GetStreamWriter(string path);

        /// <summary>
        /// Flushes all open streams.
        /// </summary>
        void FlushAllStreams();
    }
}
