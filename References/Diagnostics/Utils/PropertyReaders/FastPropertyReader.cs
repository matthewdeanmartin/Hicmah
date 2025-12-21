using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{

    /// <summary>
    /// Wraps an existing PropertyReader and accesses a Property of the returned value
    /// </summary>
    public class FastPropertyReader : PropertyReader
    {
        private readonly FastPropertyGetter _fastPropertyGetter;
        private readonly PropertyReader _wrappedPropertyReader;
        private readonly IComparator _comparator;

        /// <summary>
        /// Contructs a new instance of the FastPropertyReader
        /// </summary>
        /// <param name="wrappedPropertyReader">The <see cref="PropertyReader"/> to be wrapped</param>
        /// <param name="propertyName">The name of the property to be retrieved from the result</param>
        public FastPropertyReader(PropertyReader wrappedPropertyReader, string propertyName)
        {
            _fastPropertyGetter = new FastPropertyGetter(propertyName, wrappedPropertyReader.PropertyType);
            _wrappedPropertyReader = wrappedPropertyReader;

            if (_fastPropertyGetter.PropertyType == typeof(string))
            {
                _comparator = StringComparator.Instance;
            }
            else if (typeof(IConvertible).IsAssignableFrom(_fastPropertyGetter.PropertyType) &&
                     typeof(IComparable).IsAssignableFrom(_fastPropertyGetter.PropertyType))
            {
                _comparator = NumericComparator.Instance;
            }
            else
            {
                _comparator = ObjectComparator.Instance;
            }
        }


        /// <inheritdoc />
        public override Type PropertyType
        {
            get { return _fastPropertyGetter.PropertyType; }
        }

        /// <inheritdoc />
        public override IComparator Comparator
        {
            get { return _comparator; }
        }

        /// <inheritdoc />
        public override bool TryGetValue(out object value, System.Diagnostics.TraceEventCache cache, string source, System.Diagnostics.TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            object result = null;
            if (_wrappedPropertyReader.TryGetValue(out result, cache, source, eventType, id, formatOrMessage, data))
            {
                PropertyResult pr = _fastPropertyGetter.GetValue(result);
                value = pr.Data;
                return pr.ObjectMatched;
            }
            value = null;
            return false;
            
        }
    }
}
