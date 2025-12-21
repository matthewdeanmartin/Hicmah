// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System;
using System.Globalization;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// A numeric comparitor
    /// </summary>
    public class NumericComparator : IComparator
    {
        private static readonly NumericComparator _instance = new NumericComparator();

        /// <summary>
        /// Gets a shared singleton instance of the NumericComparator
        /// </summary>
        public static NumericComparator Instance
        {
            get { return _instance;}
        }

        private const Operations _supportedOperations = Operations.Equals
                                                        | Operations.GreaterThan | Operations.GreaterThanOrEqualTo
                                                        | Operations.LessThanOrEqualTo | Operations.LessThan
                                                        | Operations.NotEquals;
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
            // TODO (JT): Consider behaviour here if is null? Is this appropriate?
            if (null == value1)
                throw new ArgumentNullException("value1");

            IComparable comp1 = value1 as IComparable;

            if (comp1 == null)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                                                          Resources.TypeDoesNotImplementIComparable,
                                                          value1.GetType(),
                                                          typeof (NumericComparator)));
            }

            int comparison = comp1.CompareTo(value2);

            switch (operation)
            {
                case Operation.Equals:
                    return comparison == 0;
                case Operation.NotEquals:
                    return comparison != 0;
                case Operation.GreaterThan:
                    return comparison > 0;
                case Operation.GreaterThanOrEqualTo:
                    return comparison >= 0;
                case Operation.LessThan:
                    return comparison < 0;
                case Operation.LessThanOrEqualTo:
                    return comparison <= 0;
                default:
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                                                      Resources.OperationNotSupported,
                                                                      operation,
                                                                      typeof (NumericComparator).FullName,
                                                                      SupportedOperations));
            }
        }
    }
}