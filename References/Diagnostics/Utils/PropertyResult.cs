// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// Describes the result of <see cref="FastPropertyGetter" /> <see cref="FastPropertyGetter.GetValue"/> call.
    /// </summary>
    /// <remarks>
    /// When the <see cref="FastPropertyGetter" /> executes we pass back a PropertyResult instance which indicates
    /// whether an object was found (the ObjectMatched property) and if so then this also includes the object itself
    /// in the Data property. It has been done this way to avoid throwing exceptions from within the FPG as it's
    /// faster for us to do it this way
    /// </remarks>
    public class PropertyResult
    {
        /// <summary>
        /// Construct the property result
        /// </summary>
        public PropertyResult()
        {
            ObjectMatched = false;
        }

        /// <summary>
        /// Gets or sets whether the GetValue call successfully match a value
        /// </summary>
        public bool ObjectMatched
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type returned by the GetValue call.
        /// </summary>
        public object Data
        {
            get { return _data; }
            set
            {
                _data = value;
                ObjectMatched = true;
            }
        }

        private object _data;
    }
}