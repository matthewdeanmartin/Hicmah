using System;
using System.Collections.Generic;
using System.Text;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// A class (not a struct) used to lazily instantiate instances of type <typeparamref name="T"/>. Similar to the 
    /// LazyInit coming in the Parallel extensions but only supports the EnsureSingleExecution mode. The type is created
    /// when the <see cref="Instance"/> propety is first accessed.
    /// </summary>
    /// <typeparam name="T">The type to be lazily instantiated (must have a public default constructor)</typeparam>
    public class LazyInit<T> where T : new()
    {
        private readonly object _lock = new object();
        private T _instance;

        /// <summary>
        /// Creates an instance of type T the first time it's called. The same instance
        /// will be returned thereafter.
        /// </summary>
        public T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
