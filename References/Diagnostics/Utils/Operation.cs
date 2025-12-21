// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Globalization;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// A list of operations that can be specified when calling an <see cref="IComparator" />
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue"),
     SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags")]
    [TypeConverter(typeof(OperationConverter))]
    public enum Operation
    {
        /// <summary>
        /// Compare for equality
        /// </summary>
        Equals = 1,
        /// <summary>
        /// Compare for inequality
        /// </summary>
        NotEquals = 2,
        /// <summary>
        /// Is the object null
        /// </summary>
        IsNull = 4,
        /// <summary>
        /// Check if the object is not null
        /// </summary>
        IsNotNull = 8,
        /// <summary>
        /// Does the object contain another object (typically a substring)
        /// </summary>
        Contains = 16,
        /// <summary>
        /// Does the object start with a particular value
        /// </summary>
        StartsWith = 32,
        /// <summary>
        /// Does the object end withh a particular value
        /// </summary>
        EndsWith = 64,
        /// <summary>
        /// Is the object greater than
        /// </summary>
        GreaterThan = 128,
        /// <summary>
        /// Is the object greater that or equal to
        /// </summary>
        GreaterThanOrEqualTo = 256,
        /// <summary>
        /// Is the object less than
        /// </summary>
        LessThan = 512,
        /// <summary>
        /// Is the object less than or equal to
        /// </summary>
        LessThanOrEqualTo = 1024,
        /// <summary>
        /// Perform a regular expression match
        /// </summary>
        RegexMatch = 2048,
    }

    /// <summary>
    /// This type converter is used to simplify the definition of operation values in the config file
    /// </summary>
    /// <remarks>
    /// This converter allows the user to type in operation names such as "&gt;", "&lt;" into the config file, rather than
    /// having to specify the full value such as GreaterThan.
    /// </remarks>
    internal class OperationConverter : EnumConverter
    {
        /// <summary>
        /// Construct the converter
        /// </summary>
        public OperationConverter() : base(typeof(Operation)) { }

        /// <summary>
        /// Convert from a value
        /// </summary>
        /// <param name="context">The conversion context</param>
        /// <param name="culture">The current culture</param>
        /// <param name="value">The value to convert</param>
        /// <returns>The value converted to an Operation value, or an exception if this conversion cannot take place</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            object retVal = null;
            string v = value as string;

            if (null != v)
            {
                switch (v)
                {
                    case "=":
                        {
                            retVal = Operation.Equals;
                            break;
                        }
                    case "!=":
                        {
                            retVal = Operation.NotEquals;
                            break;
                        }
                    case ">":
                        {
                            retVal = Operation.GreaterThan;
                            break;
                        }
                    case ">=":
                        {
                            retVal = Operation.GreaterThanOrEqualTo;
                            break;
                        }
                    case "<":
                        {
                            retVal = Operation.LessThan;
                            break;
                        }
                    case "<=":
                        {
                            retVal = Operation.LessThanOrEqualTo;
                            break;
                        }
                }
            }

            if ( null == retVal)
                retVal = base.ConvertFrom(context, culture, value);

            return retVal;
        }
    }

    /// <summary>
    /// Specified a list of <see cref="Operation" /> that are supported by a specific <see cref="IComparator" />
    /// </summary>
    [Flags]
    public enum Operations
    {
        /// <summary>
        /// Compare for equality
        /// </summary>
        Equals = 1,
        /// <summary>
        /// Compare for inequality
        /// </summary>
        NotEquals = 2,
        /// <summary>
        /// Is the object null
        /// </summary>
        IsNull = 4,
        /// <summary>
        /// Check if the object is not null
        /// </summary>
        IsNotNull = 8,
        /// <summary>
        /// Does the object contain another object (typically a substring)
        /// </summary>
        Contains = 16,
        /// <summary>
        /// Does the object start with a particular value
        /// </summary>
        StartsWith = 32,
        /// <summary>
        /// Does the object end withh a particular value
        /// </summary>
        EndsWith = 64,
        /// <summary>
        /// Is the object greater than
        /// </summary>
        GreaterThan = 128,
        /// <summary>
        /// Is the object greater that or equal to
        /// </summary>
        GreaterThanOrEqualTo = 256,
        /// <summary>
        /// Is the object less than
        /// </summary>
        LessThan = 512,
        /// <summary>
        /// Is the object less than or equal to
        /// </summary>
        LessThanOrEqualTo = 1024,
        /// <summary>
        /// Perform a regular expression match
        /// </summary>
        RegexMatch = 2048,
    }
}