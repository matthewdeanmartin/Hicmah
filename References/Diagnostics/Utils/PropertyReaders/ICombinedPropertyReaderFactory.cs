// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// Interface used to create property readers based on some form of token
    /// </summary>
    public interface ICombinedPropertyReaderFactory
    {
        /// <summary>
        /// Creates a <see cref="PropertyReader"/> based on a combined token
        /// </summary>
        /// <param name="combinedToken">The value of the combined token to be parsed</param>
        /// <param name="propertyReaderFactory">The factory to be used to create individual tokens</param>
        /// <returns></returns>
        PropertyReader Create(string combinedToken, IPropertyReaderFactory propertyReaderFactory);
    }
}
