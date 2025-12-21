// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using Ukadc.Diagnostics.Configuration;
using Ukadc.Diagnostics.Filters.Configuration;

namespace Ukadc.Diagnostics.Filters
{
    /// <summary>
    /// An implementation of <see cref="TraceFilter"/> from <see cref="System.Diagnostics"/> that allows multiple
    /// <see cref="TraceFilter"/>'s to be combined using the boolean operations 'and' &amp; 'or'.
    /// </summary>
    public class MultiFilter : TraceFilter
    {
        private readonly MultiFilterGroup _filterGroup;

        /// <summary>
        /// Creates a new <see cref="MultiFilter"/> from configuration using the group name in configuration
        /// </summary>
        /// <param name="groupName">The name of the group in configuration</param>
        public MultiFilter(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentNullException("groupName");

            UkadcDiagnosticsSection multiFilterSection = UkadcDiagnosticsSection.ReadConfigSection();
            FilterGroupElement filterGroupConfigElement = multiFilterSection.FilterGroups[groupName];
            if (null == filterGroupConfigElement)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.CurrentCulture, Resources.MultiFilterGroupError, groupName));
            }

            _filterGroup = new MultiFilterGroup(filterGroupConfigElement);
            Validate();
        }

        /// <summary>
        /// Creates a new <see cref="MultiFilter"/> with a specified <see cref="MultiFilterGroup"/>
        /// </summary>
        /// <param name="filterGroup"></param>
        public MultiFilter(MultiFilterGroup filterGroup)
        {
            _filterGroup = filterGroup;
            Validate();
        }

        private void Validate()
        {
            if (_filterGroup.Count == 0)
                throw new ConfigurationErrorsException(Resources.MultiFilterMustContainOneFilter);
        }
        
        /// <summary>
        /// Based on the filters defined, work out whether the given event should be written to the trace listener
        /// </summary>
        /// <param name="cache">The trace data</param>
        /// <param name="source">The trace source</param>
        /// <param name="eventType">The type of event being logged</param>
        /// <param name="id">A unique ID for the event</param>
        /// <param name="formatOrMessage">A string format specification or literal string</param>
        /// <param name="args">Optional arguments included in the trace data</param>
        /// <param name="data1">An optional object included in the trace data</param>
        /// <param name="data">An optional collection of objects to be included in the trace data</param>
        /// <returns>True if the event should be logged</returns>
        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id,
                                         string formatOrMessage, object[] args, object data1, object[] data)
        {
            bool andResult = true;

            foreach (MultiFilterMember member in _filterGroup)
            {
                TraceFilter filter = member.Filter;
                bool localResult = filter.ShouldTrace(cache, source, eventType, id, formatOrMessage, args, data1, data);
                if (member.Negate)
                {
                    localResult = !localResult;
                }
                if (localResult && _filterGroup.Logic == FilterGroupLogic.Or)
                {
                    return true;
                }
                andResult = andResult && localResult;
            }

            return andResult;
        }
    }
}