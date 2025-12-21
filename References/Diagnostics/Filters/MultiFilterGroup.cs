// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Ukadc.Diagnostics.Filters.Configuration;
using Ukadc.Diagnostics.Utils;

namespace Ukadc.Diagnostics.Filters
{
    /// <summary>
    /// Represents a list of filters to be combined using the Logic property
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
    public class MultiFilterGroup : Collection<MultiFilterMember>
    {
        /// <summary>
        /// Construct the class
        /// </summary>
        public MultiFilterGroup()
        {
        }

        /// <summary>
        /// Creates a FilterMember so that the TraceFilter can be added to the FilterGroup (Negate defaults to false)
        /// </summary>
        /// <param name="filter">The TraceFilter to be added</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the filter argument is null</exception>
        public void Add(TraceFilter filter)
        {
            Add(filter, false);
        }

        /// <summary>
        /// Creates a FilterMember so that the TraceFilter can be added to the FilterGroup
        /// </summary>
        /// <param name="filter">The TraceFilter to be added</param>
        /// <param name="negate">Whether the result of the Filter should be inversed</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the filter argument is null</exception>
        public void Add(TraceFilter filter, bool negate)
        {
            if (null == filter)
                throw new ArgumentNullException("filter");

            MultiFilterMember member = new MultiFilterMember(filter, negate);
            this.Add(member);
        }

        /// <summary>
        /// Constructs a filter gourp using the filterGroupConfig configuration element
        /// </summary>
        /// <param name="filterGroupConfig">the configuration element</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the filterGroupConfig argument is null</exception>
        public MultiFilterGroup(FilterGroupElement filterGroupConfig)
        {
            if (null == filterGroupConfig)
                throw new ArgumentNullException("filterGroupConfig");

            Logic = filterGroupConfig.Logic;

            for (int i = 0; i < filterGroupConfig.Filters.Count; i++)
            {
                FilterElement element = filterGroupConfig.Filters[i];
                TraceFilter filter =
                    (TraceFilter)
                    TraceUtils.GetRuntimeObject(element.TypeName, typeof (TraceFilter), element.InitializeData);
                MultiFilterMember member = new MultiFilterMember(filter, element.Negate);
                this.Add(member);
            }
        }

        /// <summary>
        /// The logical operator applied to the results of the member filters to produce a group result
        /// </summary>
        public FilterGroupLogic Logic
        {
            get;
            set;
        }
    }
}