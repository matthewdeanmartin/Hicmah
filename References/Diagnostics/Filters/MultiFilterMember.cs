// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Diagnostics;

namespace Ukadc.Diagnostics.Filters
{
    /// <summary>
    /// This class defines a member of the MultiFilterGroup
    /// </summary>
    public class MultiFilterMember
    {
        /// <param name="filter">The TraceFilter included in the MultiFilter's specified FilterGroup</param>
        /// <param name="negate">Indicates whether the result of the filter should be inversed when ShouldTrace is evaluated (i.e True -> False and False - True). 
        /// Specify true to negate the result or false to leave as is.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the filter argument is null</exception>
        public MultiFilterMember(TraceFilter filter, bool negate)
        {
            if (null == filter)
                throw new ArgumentNullException("filter");

            Negate = negate;
            Filter = filter;
        }

        /// <summary>
        /// Indicates whether the result of the filter should be inversed when ShouldTrace is evaluated (i.e True -> False and False - True). 
        /// Specify true to negate the result or false to leave as is.
        /// </summary>
        public bool Negate
        {
            get;
            set;
        }

        /// <summary>
        /// The TraceFilter included in the MultiFilter's specified FilterGroup
        /// </summary>
        public TraceFilter Filter
        {
            get;
            set;
        }
    }
}