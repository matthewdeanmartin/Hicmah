// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using Ukadc.Diagnostics.Configuration;
using System.Collections.Generic;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{
    /// <summary>
    /// A factory class that dispenses property reader instances
    /// </summary>
    public interface IPropertyReaderFactory
    {
        /// <summary>
        /// Add a token and define which property reader should be used for that token
        /// </summary>
        /// <param name="token">The token</param>
        /// <param name="propertyReader">The property reader used to return the value of the token at runtime</param>
        void AddToken(string token, PropertyReader propertyReader);

        /// <summary>
        /// Return a dynamic property reader
        /// </summary>
        /// <param name="dataProperty">The data property</param>
        /// <returns>A property reader</returns>
        PropertyReader Create(DataPropertyElement dataProperty);

        /// <summary>
        /// Return a property reader for the abstract token element
        /// </summary>
        /// <param name="abstractTokenElement">An abstract token element</param>
        /// <returns>A property reader</returns>
        PropertyReader Create(AbstractTokenElement abstractTokenElement);

        /// <summary>
        /// Create a property reader for the passed token.
        /// </summary>
        /// <param name="tokenString">The token, such as {DateTime:YYYY} or {MachineName}</param>
        /// <returns>A property reader</returns>
        PropertyReader Create(string tokenString);

        /// <summary>
        /// Create a property reader for the passed token.
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>A property reader</returns>
        PropertyReader Create(Token token);

        /// <summary>
        /// Return a combined reader for the passed combined tokens
        /// </summary>
        /// <param name="combinedToken">The combined token(s)</param>
        /// <returns>A property reader for the passed combined tokens</returns>
        PropertyReader CreateCombinedReader(string combinedToken);

        /// <summary>
        /// Returns the list of all known tokens
        /// </summary>
        IEnumerable<String> TokenNames { get; }

        /// <summary>
        /// Indicates whether the specified token is available
        /// </summary>
        /// <param name="tokenName">The name of the token (without {})</param>
        /// <returns>True if the specified token is supported</returns>
        bool ContainsToken(string tokenName);
    }
}
