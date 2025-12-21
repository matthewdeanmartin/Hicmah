// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using Ukadc.Diagnostics.Utils.PropertyReaders;

namespace Ukadc.Diagnostics.Listeners
{
    /// <summary>
    /// Represents a parameter used by the <see cref="SqlTraceListener"/>
    /// </summary>
    public class SqlTraceParameter
    {
        /// <summary>
        /// Construct a SqlTraceParameter
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <param name="propertyReader">The property reader used to read it</param>
        /// <param name="callToString">True if .ToString() should be called</param>
        public SqlTraceParameter(string name, PropertyReader propertyReader, bool callToString)
        {
            Name = name;
            PropertyReader = propertyReader;
            CallToString = callToString;
        }

        /// <summary>
        /// The name of the parameter (e.g. @Message)
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="PropertyReader"/> used to get the value passed as this parameter value to the <see cref="SqlTraceListener"/>
        /// </summary>
        public PropertyReader PropertyReader
        {
            get;
            set;
        }

        /// <summary>
        /// Calls <see cref="object.ToString"/> on the value before passing it as the parameter value ot the <see cref="SqlTraceListener"/>
        /// </summary>
        public bool CallToString
        {
            get;
            set;
        }
    }
}