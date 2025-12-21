// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// Interface used when comparing things
    /// </summary>
    public interface IComparator
    {
        /// <summary>
        /// Gets a 'list' of <see cref="Operation"/>s supported by this Comparator
        /// </summary>
        Operations SupportedOperations { get; }

        /// <summary>
        /// Compares to values using the specified operations (this should be one of the <see cref="SupportedOperations"/>)
        /// </summary>
        /// <param name="operation">The type of <see cref="Operation" /> used for the comparison</param>
        /// <param name="value1">The first value in the comparison</param>
        /// <param name="value2">The second value in the comparison</param>
        /// <returns>true or false</returns>
        bool Compare(Operation operation, object value1, object value2);
    }
}