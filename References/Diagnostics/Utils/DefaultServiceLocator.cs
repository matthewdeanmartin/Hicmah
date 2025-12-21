using System;
using System.Collections.Generic;
using System.Text;
using Ukadc.Diagnostics.Utils.PropertyReaders;
using Ukadc.Diagnostics.Listeners;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// The service locator used by the code
    /// </summary>
    public class DefaultServiceLocator : ServiceLocator
    {
        /// <summary>
        /// Return the singleton instance
        /// </summary>
        public static ServiceLocator Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Add some default services to the service locator
        /// </summary>
        static DefaultServiceLocator()
        {
            _instance.RegisterType<IInternalLogger, InternalLogger>();
            _instance.RegisterType<ICombinedPropertyReaderFactory, CombinedPropertyReaderFactory>();
            _instance.RegisterType<IPropertyReaderFactory, PropertyReaderFactory>();
            _instance.RegisterType<IStreamWriterCache, StreamWriterCache>();
        }

        /// <summary>
        /// Return a service
        /// </summary>
        /// <typeparam name="T">The type of service requested</typeparam>
        /// <returns>The service instance</returns>
        public static T GetService<T>()
        {
            return _instance.GetService<T>();
        }

        /// <summary>
        /// Holds the static singleton instance
        /// </summary>
        private static readonly ServiceLocator _instance = new ServiceLocator();
    }
}
