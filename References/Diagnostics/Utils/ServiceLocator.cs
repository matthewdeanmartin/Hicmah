using System;
using System.Collections.Generic;
using System.Text;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// A delegate which is used when constructing an instance of the T type
    /// </summary>
    /// <typeparam name="T">A generic type</typeparam>
    /// <returns>An instance of the T type</returns>
    public delegate T BuildUp<T>();

    /// <summary>
    /// This object is used to provide services to consumers
    /// </summary>
    /// <remarks>
    /// This object implements the <see cref="IServiceProvider"/> interface which can be used to pass details of services to
    /// consumers. It pre-dates all this dependency injection clobber and does pretty much the same job but in a slightly
    /// different manner. 
    /// </remarks>
    public class ServiceLocator : IServiceProvider
    {
        /// <summary>
        /// Get the requested service
        /// </summary>
        /// <param name="serviceType">The type of service being requested</param>
        /// <returns>An instance of that service</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the service type does not exist in this service provider</exception>
        public object GetService(Type serviceType)
        {
            return GetServiceBuilder(serviceType).BuildObject();
        }

        /// <summary>
        /// Returns an instance of type <typeparamref name="T"/> as per the mapping registered with the locator
        /// </summary>
        /// <typeparam name="T">The type of service requested</typeparam>
        /// <returns>An instance of that service</returns>
        /// <exception cref="System.InvalidOperationException">Thrown if the service type does not exist in this service provider</exception>
        public T GetService<T>()
        {
            BuildUp<T> del = GetBuildDelegate<T>();
            return del();
        }

        /// <summary>
        /// Registers a type mapping in the locator where the <paramref name="buildMethod"/> delegate will be called when 
        /// <see cref="GetService"/> is called for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        /// <param name="buildMethod">The delegate called to build instances of type <typeparamref name="T"/></param>
        /// <exception cref="InvalidOperationException">Throw when this service type has already been registered with the container</exception>
        public void RegisterType<T>(BuildUp<T> buildMethod)
        {
            Type type = typeof(T);

            lock (_dictionaryLock)
            {
                if (_dictionary.ContainsKey(type))
                {
                    throw new InvalidOperationException(string.Format(
                        Resources.TypeAlreadyRegistered,
                        typeof(T)));
                }
                _dictionary.Add(type, new ServiceBuilder<T>(buildMethod));
            }
        }

        /// <summary>
        /// Registers an instance of type <typeparamref name="T"/> with the locator that will be returned for all calls to <see cref="GetService"/> for this type
        /// </summary>
        /// <typeparam name="T">The type implemented by the instance</typeparam>
        /// <param name="instance">The instance of type <typeparamref name="T"/></param>
        public void RegisterType<T>(T instance)
        {
            RegisterType<T>(delegate { return instance; });
        }

        /// <summary>
        /// Registers a type mapping in the locator where <typeparamref name="TTo"/> : <typeparamref name="TFrom"/>. The concrete type must have a public 
        /// default constructor. Calls to <see cref="GetService"/>
        /// for the type <typeparamref name="TFrom"/> will return a single instance of TTo that will be lazily created.
        /// </summary>
        /// <typeparam name="TFrom">The service type</typeparam>
        /// <typeparam name="TTo">The concrete implementation of the service type (<typeparamref name="TFrom"/>)</typeparam>
        public void RegisterType<TFrom, TTo>() where TTo : TFrom, new()
        {
            RegisterType<TFrom>(CreateLazyBuildUp<TFrom, TTo>());
        }

        /// <summary>
        /// Temporarily registers a new type mapping in the locator where the <paramref name="buildMethod"/> delegate will be called when 
        /// <see cref="GetService"/> is called for type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The service type</typeparam>
        public IDisposable OverrideType<T>(T instance)
        {
            return OverrideType<T>(delegate { return instance; });
        }

        /// <summary>
        /// Temporarily registers a new instance of type <typeparamref name="T"/> with the locator that will be returned 
        /// for all calls to <see cref="GetService"/> for this type
        /// </summary>
        /// <typeparam name="T">The type implemented by the instance</typeparam>
        /// <param name="newBuildMethod">The method used to construct an instance of T</param>
        public IDisposable OverrideType<T>(BuildUp<T> newBuildMethod)
        {
            BuildUp<T> oldBuildMethod = GetBuildDelegate<T>();
            OverrideDisposer<T> disposer = new OverrideDisposer<T>(oldBuildMethod, this);
            ReplaceType<T>(newBuildMethod);
            return disposer;
        }

        /// <summary>
        /// Temporarily tegisters a new type mapping in the locator where <typeparamref name="TTo"/> : <typeparamref name="TFrom"/>. 
        /// The concrete type must have a public default constructor. Calls to <see cref="GetService"/>
        /// for the type <typeparamref name="TFrom"/> will return a single instance of TTo that will be lazily created.
        /// </summary>
        /// <typeparam name="TFrom">The service type</typeparam>
        /// <typeparam name="TTo">The concrete implementation of the service type (<typeparamref name="TFrom"/>)</typeparam>
        public IDisposable OverrideType<TFrom, TTo>() where TTo : TFrom, new()
        {
            return OverrideType<TFrom>(CreateLazyBuildUp<TFrom, TTo>());
        }

        private void ReplaceType<T>(BuildUp<T> buildMethod)
        {
            lock (_dictionaryLock)
            {
                _dictionary[typeof(T)] = new ServiceBuilder<T>(buildMethod);
            };
        }

        private BuildUp<T> GetBuildDelegate<T>()
        {
            ServiceBuilder<T> builder = (ServiceBuilder<T>)GetServiceBuilder(typeof(T));
            return builder.BuildUp;
        }

        private IServiceBuilder GetServiceBuilder(Type type)
        {
            if (!_dictionary.ContainsKey(type))
            {
                throw new InvalidOperationException(string.Format(
                    Resources.TypeNotRegistered,
                    type));
            }
            return _dictionary[type];
        }

        private class OverrideDisposer<T> : IDisposable
        {
            private ServiceLocator _locator;
            private BuildUp<T> _oldBuildMethod;

            internal OverrideDisposer(BuildUp<T> oldBuildMethod, ServiceLocator locator)
            {
                _oldBuildMethod = oldBuildMethod;
                _locator = locator;
            }

            public void Dispose()
            {
                _locator.ReplaceType<T>(_oldBuildMethod);
            }
        }

        /// <summary>
        /// Creates a build up delegate for use with a <see cref="ServiceLocator"/>.
        /// </summary>
        /// <typeparam name="TFrom">The service type (superclass)</typeparam>
        /// <typeparam name="TTo">The concrete implementation (subclass)</typeparam>
        /// <returns>A build up delegate</returns>
        private static BuildUp<TFrom> CreateLazyBuildUp<TFrom, TTo>() where TTo : TFrom, new()
        {
            LazyInit<TTo> init = new LazyInit<TTo>();

            BuildUp<TFrom> del = delegate
            {
                return (TFrom)init.Instance;
            };
            return del;
        }
     

        private class ServiceBuilder<T> : IServiceBuilder
        {
            public ServiceBuilder(BuildUp<T> buildMethod)
            {
                BuildUp = buildMethod;
            }

            public BuildUp<T> BuildUp { get; set; }
            
            public object BuildObject()
            {
                return this.BuildUp();
            }
        }

        private interface IServiceBuilder
        {
            object BuildObject();
        }

        /// <summary>
        /// Object used to synchronise access to the service dictionary
        /// </summary>
        private readonly object _dictionaryLock = new object();

        /// <summary>
        /// Dictionary of services
        /// </summary>
        private readonly Dictionary<Type, IServiceBuilder> _dictionary = new Dictionary<Type, IServiceBuilder>();
    }
}
