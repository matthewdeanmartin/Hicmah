// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;
using System.Globalization;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// A <see cref="PropertyReader"/> that uses the <see cref="FastPropertyGetter"/> to dynamically access
    /// properties at run-time in a performant manner.
    /// </summary>
    public class DynamicPropertyReader : PropertyReader
    {
        private readonly FastPropertyGetter _fastPropertyGetter;
        private readonly IComparator _comparator;

        /// <summary>
        /// Creates a new instance of a DynamicPropertyReader
        /// </summary>
        /// <param name="sourceType">The assembly qualified name of the type against which the property should be invoked</param>
        /// <param name="propertyName">The name of the property to be 'gotten' and evaluated against</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the sourceType or propertyName arguments are null or empty strings</exception>
        /// <exception cref="System.TypeLoadException">Thrown if the sourceType is not a valid .NET type, or cannot be loaded</exception>
        public DynamicPropertyReader(string sourceType, string propertyName)
        {
            if (string.IsNullOrEmpty(sourceType))
                throw new ArgumentNullException("sourceType");
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException("propertyName");

            // Attmpt to get the type, and throw a TypeLoadException if not found
            Type type = Type.GetType(sourceType, true);

            _fastPropertyGetter = new FastPropertyGetter(propertyName, type);

            if (_fastPropertyGetter.PropertyType == typeof (string))
            {
                _comparator = StringComparator.Instance;
            }
            else if (typeof (IConvertible).IsAssignableFrom(_fastPropertyGetter.PropertyType) &&
                     typeof (IComparable).IsAssignableFrom(_fastPropertyGetter.PropertyType))
            {
                _comparator = NumericComparator.Instance;
            }
            else
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                                                  Resources.DoesNotImplementRightInterfaces,
                                                                  propertyName, sourceType));
            }
        }

        /// <summary>
        /// Searches the objects in the "data" parameter for in an instance of the specified sourceType
        /// and, if found, attempts to retrieve the value of the specified Property.
        /// </summary>
        /// <param name="value">Return value</param>
        /// <param name="cache">Unused</param>
        /// <param name="source">Unused</param>
        /// <param name="eventType">Unused</param>
        /// <param name="id">Unused</param>
        /// <param name="formatOrMessage">Unused</param>
        /// <param name="data">Data to be searched</param>
        /// <returns>False if no match found, true otherwise</returns>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source,
                                         TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            value = null;

            if (null == data)
                return false;

            foreach (object obj in data)
            {
                PropertyResult result = _fastPropertyGetter.GetValue(obj);
                if (result.ObjectMatched)
                {
                    value = result.Data;
                    return result.ObjectMatched;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the type of the property
        /// </summary>
        public override Type PropertyType
        {
            get { return _fastPropertyGetter.PropertyType; }
        }

        /// <summary>
        /// Get the comparitor for the property
        /// </summary>
        public override IComparator Comparator
        {
            get { return _comparator; }
        }
    }
}