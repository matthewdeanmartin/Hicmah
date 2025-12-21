using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using Ukadc.Diagnostics.Utils;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// Concrete implementation of the <see cref="Ukadc.Diagnostics.Listeners.IStreamWriterCache">IStreamWriterCache</see> interface
    /// </summary>
    public class StreamWriterCache : IStreamWriterCache
    {
        private ReaderWriterLock _lock = new ReaderWriterLock();
        private readonly Dictionary<string, CachedStream> _streamCache = new Dictionary<string, CachedStream>();

        private static string NormalizePath(string path)
        {
            if (Directory.Exists(path))
            {
                return path;
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
        }

        /// <summary>
        /// Return a stream writer for the passed path name
        /// </summary>
        /// <param name="path">The path name</param>
        /// <returns>An existing or new stream writer</returns>
        public StreamWriter GetStreamWriter(string path)
        {
            CachedStream cs = null;

            using(Locker.AcquireReaderLock(_lock))
            {
                if (!_streamCache.TryGetValue(path, out cs))
                {
                    using (Locker.UpgradeToWriterLock(_lock))
                    {
                        Stream stream = GetStream(path);
                        cs = new CachedStream() { StreamWriter = new StreamWriter(stream) };
                        _streamCache.Add(path, cs);
                    }
                }
            }

            cs.LastAccessTime = DateTime.Now;
            return cs.StreamWriter;
        }

        /// <summary>
        /// Returns a FileStream (open or create, write, read) - can be overriden for unit testing purposes
        /// </summary>
        /// <param name="path">Path for the stream</param>
        /// <returns>A FileStream</returns>
        protected virtual Stream GetStream(string path)
        {
            string actualPath = NormalizePath(path);
            FileStream fs = new FileStream(actualPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
            // This seek is important, or the file is overwritten.
            fs.Seek(0, SeekOrigin.End);
            return fs;
        }

        /// <summary>
        /// Searches the cache of StreamWriters and disposes and removes any that haven't been used accessed since
        /// the notUsedSince parameter.
        /// </summary>
        /// <param name="notUsedSince">The time </param>
        public virtual void ClearOldStreams(DateTime notUsedSince)
        {
            Queue<string> _removals = new Queue<string>();

            using(Locker.AcquireWriterLock(_lock))
            {
                foreach (KeyValuePair<string, CachedStream> kvp in _streamCache)
                {
                    if (kvp.Value.LastAccessTime < notUsedSince)
                    {
                        kvp.Value.StreamWriter.Dispose();
                        // can't remove items whilst enumerating so remember them for later
                        _removals.Enqueue(kvp.Key);
                    }
                }

                while (_removals.Count > 0)
                {
                    _streamCache.Remove(_removals.Dequeue());
                }
            }
        }

        private class CachedStream
        {
            internal CachedStream() { }

            /// <summary>
            /// The last time an item was accessed
            /// </summary>
            public DateTime LastAccessTime { get; set; }
            /// <summary>
            /// The cached StreamWriter
            /// </summary>
            public StreamWriter StreamWriter { get; set; }
        }

        public void FlushAllStreams()
        {
            using (Locker.AcquireReaderLock(_lock))
            {
                foreach (var stream in _streamCache.Values)
                {
                    stream.StreamWriter.Flush();
                }
            }

        }
    }

    public static class Locker
    {
        public static IDisposable AcquireReaderLock(ReaderWriterLock rwl)
        {
            rwl.AcquireReaderLock(Timeout.Infinite);
            return new Disposer(() => rwl.ReleaseReaderLock());
        }

        public static IDisposable AcquireWriterLock(ReaderWriterLock rwl)
        {
            rwl.AcquireWriterLock(Timeout.Infinite);
            return new Disposer(() => rwl.ReleaseWriterLock());
        }

        public static IDisposable UpgradeToWriterLock(ReaderWriterLock rwl)
        {
            var lockCookie = rwl.UpgradeToWriterLock(Timeout.Infinite);
            return new Disposer(() => rwl.DowngradeFromWriterLock(ref lockCookie));
        }
    }
}
