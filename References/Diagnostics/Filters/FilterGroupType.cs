// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

namespace Ukadc.Diagnostics.Filters
{
    /// <summary>
    /// The logic by which the member filters of the filter group are combined
    /// </summary>
    public enum FilterGroupLogic
    {
        /// <summary>
        ///  Logical AND (all filters must return true)
        /// </summary>
        And,
        /// <summary>
        /// Logical OR (at least one filter must return true)
        /// </summary>
        Or,
    }
}