using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// The FormattedPropertyReader wraps an existing instance of a property reader to apply a format string
    /// </summary>
    /// <remarks>
    /// When a token is read it may have an optional format specification string defined. The code that reads tokens looks for this
    /// format specification, and if found returns the token instance wrapped wioth an instance of the formatted property reader.
    /// <para>
    /// Consider the following token: Date:hh:mm:ss. This defines a token called 'Date' and a format specification of 'hh:mm:ss'. 
    /// What we do internally is construct an instance of the <see cref="DateTimePropertyReader"/> class, and wrap this object
    /// in an instance of FormattedPropertyReader (which includes the format specification string). Then when the TryGetValue
    /// method is called, we pass the object returned from the DateTimePropertyReader through a call to .ToString(), using
    /// the format specification string as a parameter.
    /// </para>
    /// <para>
    /// This way we can add a format specification to any of the tokens defined, either by us or as extensions in your own code.
    /// </para>
    /// </remarks>
    public class FormattedPropertyReader : StringPropertyReader
    {
        private readonly PropertyReader _wrappedPropertyReader;

        /// <summary>
        /// Stores the format specification string
        /// </summary>
        public string FormatString { get; private set; }

        /// <inheritdoc />
        public FormattedPropertyReader(PropertyReader wrappedPropertyReader, string formatString)
        {
            _wrappedPropertyReader = wrappedPropertyReader;
            FormatString = formatString;
        }

        /// <inheritdoc />
        /// <remarks>
        /// This method calls the underlying property reader to obtain its value, and then if the obeject returned implements
        /// the <see cref="System.IFormattable"/> interface we then call .ToString(FormatString) on it and return the
        /// formatted version of the data.
        /// </remarks>
        public override bool TryGetValue(out object value, TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] data)
        {
            object subval = null;
            if (_wrappedPropertyReader.TryGetValue(out subval, cache, source, eventType, id, formatOrMessage, data))
            {
                value = subval;
                IFormattable formattable = subval as IFormattable;
                if (formattable != null)
                {
                    value = formattable.ToString(FormatString, CultureInfo.InvariantCulture);
                }
                return true;
            }
            value = null;
            return false;
        }
    }
}
