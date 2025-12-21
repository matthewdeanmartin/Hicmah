// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// An additional abstract class to make the creation of string base <see cref="PropertyReader"/>s (even) easier.
    /// </summary>
    public abstract class StringPropertyReader : PropertyReader
    {
        private readonly static Type _type = typeof (string);

        /// <summary>
        /// Return the property type exposed by this reader - in this case it's <see cref="System.String"/>.
        /// </summary>
        public override sealed Type PropertyType
        {
            get { return _type; }
        }

        /// <summary>
        /// Return the comparator used to compare instances of the property
        /// </summary>
        public override IComparator Comparator
        {
            get { return StringComparator.Instance; }
        }
    }
}