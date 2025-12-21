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
    public class StringComparator : IComparator
    {
        private static readonly StringComparator _instance = new StringComparator();

        /// <summary>
        /// Gets a shared singleton instance of the StringComparator
        /// </summary>
        public static StringComparator Instance
        {
            get { return _instance; }
        }

        private const Operations _supportedOperations = Operations.Equals
                                                        | Operations.NotEquals | Operations.Contains
                                                        | Operations.StartsWith | Operations.EndsWith
                                                        | Operations.IsNotNull | Operations.IsNull | Operations.RegexMatch;

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
            string string1 = (string)value1;
            string string2 = (string)value2;

            switch (operation)
            {
                case Operation.Equals:
                    return string1 == string2;
                case Operation.NotEquals:
                    return string1 != string2;
                case Operation.IsNull:
                    return string1 == null;
                case Operation.IsNotNull:
                    return string1 != null;
                case Operation.Contains:
                    return string1 != null && string2 != null && string1.Contains(string2);
                case Operation.StartsWith:
                    return
                        string1 != null && string2 != null &&
                        string1.StartsWith(string2, StringComparison.CurrentCulture);
                case Operation.EndsWith:
                    return
                        string1 != null && string2 != null && string1.EndsWith(string2, StringComparison.CurrentCulture);
                case Operation.RegexMatch:
                    return GetRegex(string2).IsMatch(string1);
                default:
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
                                                                      Resources.OperationNotSupported,
                                                                      operation,
                                                                      typeof(StringComparator).FullName,
                                                                      SupportedOperations));
            }
        }
        private readonly static Dictionary<string, Regex> _regexCache = new Dictionary<string, Regex>();
        private readonly static object _regexCacheSyncObject = new object();
        private static Regex GetRegex(string pattern)
        {
            Regex regex;
            // check if we've got the pattern cached
            if (!_regexCache.TryGetValue(pattern, out regex))
            {
                // not cached,  so lock and double check before adding
                lock (_regexCacheSyncObject)
                {
                    if (!_regexCache.TryGetValue(pattern, out regex))
                    {
                        regex = new Regex(pattern, RegexOptions.Compiled);
                        _regexCache.Add(pattern, regex);
                    }
                }
            }
            return regex;

        }
    }
}
