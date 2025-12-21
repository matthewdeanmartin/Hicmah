using System;
using System.Collections.Generic;
using System.Text;

namespace Ukadc.Diagnostics.Utils
{
    /// <summary>
    /// A token is used when defining replaceable parts of format string
    /// </summary>
    /// <remarks>
    /// <para>
    /// A token is made up of a Name and then an optional set of arguments. These arguments are a property specification and a
    /// format specification - i.e. TOKEN{.property specification}{:format specification}.
    /// </para>
    /// <para>
    /// As an example, the following are all legal token specifications...
    /// <list type="table">
    ///     <listheader>
    ///         <term>Token String</term>
    ///         <description>Description</description>
    ///     </listheader>
    ///     <item>
    ///         <term>Process</term>
    ///         <description>Return the process object</description>
    ///     </item>
    ///     <item>
    ///         <term>Process.ProcessName</term>
    ///         <description>At runtime the process name of the current executing process will be output</description>
    ///     </item>
    ///     <item>
    ///         <term>Process.TotalProcessorTime:hh:mm:ss</term>
    ///         <description>This will output the total processor time formatted as hours:minutes:seconds</description>
    ///     </item>
    /// </list>
    /// </para>
    /// <para>
    /// A property specification consists of one or more property (or field) names separated by a period.
    /// </para>
    /// <para>
    /// A format specification is a standard .NET format specification, and is simply used in a ToString() call on the property
    /// value returned at runtime. So using the last example above, the code would effectively be 
    /// ((IFormattable)object).ToString(FormatSpecification).
    /// </para>
    /// </remarks>
    public class Token
    {
        /// <summary>
        /// This method parses the passed token string and sets the properties of the Token instance accordingly
        /// </summary>
        /// <param name="tokenString">A string of the format TOKEN{.property specification}{:format specification}</param>
        public Token(string tokenString)
        {
            if (string.IsNullOrEmpty(tokenString))
            {
                throw new ArgumentException("tokenString cannot be null or empty", "tokenString");
            }

            OriginalString = tokenString;
            string formatSpecification = null;
            string property = null;

            // we need to find three sections:
            // {TokenName.SubPropertyName:FormatString}
            // both SubPropertyName and FormatString are optional
            // however, because FormatString may contain a '.' or a ':' it takes precedence
            // over SubPropertyName.
            // This char array is a (micro) optimisation to ensure we only go through the string once
            Char[] chars = tokenString.ToCharArray();
            bool openBrace = chars[0] == '{';
            bool closeBrace = chars[chars.Length-1] == '}';
            int firstColonIndex = -1;
            List<int> dotPositions = null;

            for (int i=0; i < chars.Length; i++)
            {
                Char ch = chars[i];
                
                // only interested in the first : we come to.
                if (firstColonIndex == -1 &&
                    ch == ':')
                {
                    firstColonIndex = i;
                }
                // only interested in .s before the :
                else if (firstColonIndex == -1 &&
                    ch == '.')
                {
                    if (dotPositions == null)
                    {
                        dotPositions = new List<int>();
                    }
                    dotPositions.Add(i);
                }
            }

            if (openBrace && closeBrace)
            {
                tokenString = tokenString.Substring(1, tokenString.Length - 2);
            }

            string tokenAndProps = tokenString;

            if (firstColonIndex > 0)
            {
                tokenAndProps = tokenString.Substring(0, firstColonIndex - 1);
                formatSpecification = tokenString.Substring(firstColonIndex);
            }

            int firstDotIndex = dotPositions != null ? dotPositions[0] : -1;
            
            string tokenName = tokenAndProps;

            if (firstDotIndex > 0)
            {
                tokenName = tokenAndProps.Substring(0, firstDotIndex - 1);
                property = tokenAndProps.Substring(firstDotIndex);
            }

            SubProperty = property;
            TokenName = tokenName;
            FormatString = formatSpecification;
        }

        /// <summary>
        /// Provides callers with the name of the token
        /// </summary>
        public string TokenName { get; private set; }

        /// <summary>
        /// Provides callers with the optional format specification string.
        /// </summary>
        /// <remarks>
        /// If no format specification string was used on the original token string then this property will be null.
        /// </remarks>
        public string FormatString { get; private set; }

        /// <summary>
        /// Defines the property of the token that is requested
        /// </summary>
        /// <remarks>
        /// If no sub-property was used on the original token string then this property will be null.
        /// </remarks>
        public string SubProperty { get; private set; }


        /// <summary>
        /// Stores the original string used when the token was constructed
        /// </summary>
        public string OriginalString { get; private set; }


        /// <summary>
        /// To string gives the name of the Token
        /// </summary>
        /// <returns>the name of the token</returns>
        public override string ToString()
        {
            return this.TokenName;
        }
    }
}
