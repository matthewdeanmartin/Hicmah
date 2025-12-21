// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// Defines a string comparator
    /// </summary>
    public class ObjectComparator : IComparator
    {
        private static readonly ObjectComparator _instance = new ObjectComparator();

        /// <summary>
        /// Gets a shared singleton instance of the StringComparator
        /// </summary>
        public static ObjectComparator Instance
        {
            get { return _instance; }
        }

        private const Operations _supportedOperations = Operations.Equals
                                                        | Operations.IsNotNull 
                                                        | Operations.IsNull;

        /// <summary>
        /// Gets a 'list' of <see cref="Operation"/>s supported by this Comparator
        /// </summary>
        public Operations SupportedOperations
        {
            get { return _supportedOperations; }
        }

        /// <summary>
        /// Compares to values using the specified operations (this should be one of the <see cref="SupportedOperations"/>)
        /// </summary>
        /// <param name="operation">The type of <see cref="Operation" /> used for the comparison</param>
        /// <param name="value1">The first value in the comparison</param>
        /// <param name="value2">The second value in the comparison</param>
        /// <returns>true or false</returns>
        public bool Compare(Operation operation, object value1, object value2)
        {
            switch (operation)
            {
                case Operation.Equals:
                    return object.Equals(value1, value2);
                case Operation.NotEquals:
                    return !object.Equals(value1, value2);
                case Operation.IsNull:
                    return value1 == null;
                case Operation.IsNotNull:
                    return value1 != null;
                default:
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                                                      Resources.OperationNotSupported,
                                                                      operation,
                                                                      typeof(ObjectComparator).FullName,
                                                                      SupportedOperations));
            }
        }
        
    }
}
