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
using Ukadc.Diagnostics.Utils;
using Ukadc.Diagnostics.Utils.PropertyReaders;

namespace Ukadc.Diagnostics.Filters
{
    /// <summary>
    /// A type of <see cref="TraceFilter"/> for use in <see cref="System.Diagnostics"/> that uses the <see cref="PropertyReader"/> and
    /// token system and <see cref="IComparator"/>.
    /// </summary>
    public class PropertyFilter : TraceFilter
    {
        /// <summary>
        /// Gets the <see cref="PropertyReader"/> used by this <see cref="PropertyFilter"/>
        /// </summary>
        public PropertyReader PropertyReader
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the target value against which log values are compared
        /// </summary>
        public IComparable TargetValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the default evaluation of the filter in the event of a failure to read the property (defaults to true)
        /// </summary>
        public bool DefaultEvaluation
        {
            get;
            private set;
        }

        /// <summary>
        /// The <see cref="Operation"/> used by this <see cref="PropertyFilter"/>
        /// </summary>
        public Operation Operation
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates a new <see cref="PropertyFilter"/>
        /// </summary>
        /// <param name="targetValue">The value against which log events are compared</param>
        /// <param name="operation">The operation used by this <see cref="PropertyFilter" /></param>
        /// <param name="propertyReader">The <see cref="PropertyReader"/> used by this <see cref="PropertyFilter"/></param>
        public PropertyFilter(string targetValue, Operation operation, PropertyReader propertyReader)
            : this(targetValue, operation, propertyReader, true)
        {
        }

        /// <summary>
        /// Creates a new <see cref="PropertyFilter"/>
        /// </summary>
        /// <param name="targetValue">The value against which log events are compared</param>
        /// <param name="operation">The operation used by this <see cref="PropertyFilter" /></param>
        /// <param name="propertyReader">The <see cref="PropertyReader"/> used by this <see cref="PropertyFilter"/></param>
        /// <param name="defaultEvaluation">The default evaluation of the filter in the event of a failure to read the property</param>
        public PropertyFilter(string targetValue, Operation operation, PropertyReader propertyReader, bool defaultEvaluation)
        {
            if (null == propertyReader)
                throw new ArgumentNullException("propertyReader");

            Initialize(targetValue, operation, propertyReader, defaultEvaluation);
        }

        private void Initialize(string value, Operation operation, PropertyReader propertyReader, bool defaultEvaluation)
        {
            DefaultEvaluation = defaultEvaluation;
            PropertyReader = propertyReader;
            Operation = operation;
            Validate();
            TargetValue = ParseValue(value);
        }

        /// <summary>
        /// Validates that the specified <see cref="Operation"/> is compatibile with the <see cref="IComparator"/> and also confirms
        /// that the <see cref="PropertyReader"/>'s type is IComparable and IConvertible.
        /// </summary>
        private void Validate()
        {
            if (((int) Operation & (int) PropertyReader.Comparator.SupportedOperations) <= 0)
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.OperationNotSupported,
                                  Operation, PropertyReader.Comparator, PropertyReader.Comparator.SupportedOperations));
            }
            if (!typeof (IComparable).IsAssignableFrom(PropertyReader.PropertyType) ||
                !typeof (IConvertible).IsAssignableFrom(PropertyReader.PropertyType))
            {
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, Resources.DoesNotImplementRightInterfaces,
                                  PropertyReader.GetType(),
                                  PropertyReader.PropertyType));
            }
        }

        /// <summary>
        /// Parses the string value and converts it into an IComparable type.
        /// </summary>
        /// <param name="value">The string value to be parsed</param>
        /// <returns>An <see cref="IComparable"/> type</returns>
        private IComparable ParseValue(string value)
        {
            return (IComparable) TraceUtils.ConvertToBaseTypeOrEnum(value, PropertyReader.PropertyType);
        }

        /// <summary>
        /// Creates a propertyFilter using configuration, looking for a propertyFilter element with the specified name
        /// </summary>
        /// <param name="propertyFilterName">Name of the propertyFilter element to use for configuration</param>
        /// <exception cref="System.ArgumentNullException">Thrown if the propertyFilterName argument is null</exception>
        public PropertyFilter(string propertyFilterName)
        {
            if (string.IsNullOrEmpty(propertyFilterName))
                throw new ArgumentNullException("propertyFilterName");

            UkadcDiagnosticsSection section = UkadcDiagnosticsSection.ReadConfigSection();
            PropertyFilterElement element = section.PropertyFilters[propertyFilterName];

            if (element == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format(CultureInfo.CurrentCulture, Resources.PropertyFilterNotFound,
                                  propertyFilterName));
            }

            PropertyReader = DefaultServiceLocator.GetService<IPropertyReaderFactory>().Create(element);

            Initialize(element.Value, element.Operation, PropertyReader, element.DefaultEvaluation);
        }

        /// <summary>
        /// Return true if we should trace the current event
        /// </summary>
        /// <param name="cache">The event to trace</param>
        /// <param name="source">The source of the event</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The ID of the event</param>
        /// <param name="formatOrMessage">The format or message string</param>
        /// <param name="args">A collection of optional arguments</param>
        /// <param name="data1">An optional data object</param>
        /// <param name="data">An optional data object collection</param>
        /// <returns>True if this event should be traced</returns>
        public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id,
                                         string formatOrMessage, object[] args, object data1, object[] data)
        {
            object value;

            bool valueFound = PropertyReader.TryGetValue(out value, cache, source, eventType, id, formatOrMessage, data);

            if (!valueFound)
                return DefaultEvaluation;

            return PropertyReader.Comparator.Compare(Operation, (IComparable) value, TargetValue);
        }
    }
}