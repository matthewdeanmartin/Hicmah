// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ukadc.Diagnostics.Utils.PropertyReaders
{

    /// <summary>
    /// Factory class used to create <see cref="CombinedPropertyReader"/>s from Combined Token strings.
    /// </summary>
    public class CombinedPropertyReaderFactory : ICombinedPropertyReaderFactory
    {
        /// <summary>
        /// Regular expression used to find tokens in a combined token
        /// </summary>
        private static readonly Regex _matchesTokens = new Regex("{(.[^{}]+?)}", RegexOptions.Compiled);

        /// <summary>
        /// Creates a <see cref="PropertyReader"/> by parsing the combinedToken parameter.
        /// </summary>
        /// <param name="combinedToken">A combined token</param>
        /// <param name="propertyReaderFactory">A factory object used to construct property readers from</param>
        /// <returns>Usually returns a <see cref="CombinedPropertyReader"/> but can optimise to return a specific <see cref="PropertyReader"/>
        /// or <see cref="LiteralPropertyReader"/> if there is no need for a combined version</returns>
        public PropertyReader Create(string combinedToken, IPropertyReaderFactory propertyReaderFactory)
        {
            if (combinedToken == null)
            {
                throw new ArgumentNullException("combinedToken");
            }

            MatchCollection matches = _matchesTokens.Matches(combinedToken);

            // Is the whole format string just a single token? If so, return that single property reader
            if (matches.Count == 1
                && matches[0].Length == combinedToken.Trim().Length)
            {
                Token token = new Token(matches[0].Value);
                return CreateReaderOrLiteral(propertyReaderFactory, token);
            }

            // if no tokens, just return a literalpropertyreader
            if (matches.Count == 0)
            {
                return new LiteralPropertyReader(combinedToken);
            }

            CombinedPropertyReader reader = new CombinedPropertyReader();

            int cursor = 0;

            foreach (Match match in matches)
            {
                if (match.Index > cursor)
                {
                    reader.PropertyReaders.Add(
                        new LiteralPropertyReader(combinedToken.Substring(cursor, match.Index - cursor)));
                }

                Token token = new Token(match.Value);
                reader.PropertyReaders.Add(CreateReaderOrLiteral(propertyReaderFactory, token));
                cursor = match.Index + match.Length;
            }
            if (combinedToken.Length > cursor)
            {
                reader.PropertyReaders.Add(
                    new LiteralPropertyReader(combinedToken.Substring(cursor, combinedToken.Length - cursor)));
            }


            return reader;
        }

        /// <summary>
        /// Checks to see if the PropertyReaderFactory supports the tokenName. If not, returns a LiteralPropertyReader containing
        /// fullName
        /// </summary>
        private static PropertyReader CreateReaderOrLiteral(IPropertyReaderFactory propertyReaderFactory, Token token)
        {
            if (propertyReaderFactory.ContainsToken(token.TokenName))
            {
                return propertyReaderFactory.Create(token);
            }
            else
            {
                return new LiteralPropertyReader(token.OriginalString);
            }
        }

        private List<StringMatch> FindAllMatches(string searchString, string searchFor)
        {
            List<StringMatch> matches = new List<StringMatch>();

            int cursor = 0;

            while (cursor < searchString.Length)
            {
                int index = searchString.IndexOf(searchFor, cursor, StringComparison.CurrentCulture);
                if (index == -1)
                {
                    break;
                }
                matches.Add(new StringMatch(index, searchFor));
                cursor = index + searchFor.Length;
            }

            return matches;
        }

        private class StringMatch
        {
            public StringMatch(int index, string value)
            {
                Index = index;
                Value = value;
            }

            public int Index
            {
                get;
                set;
            }

            public string Value
            {
                get;
                set;
            }

            public int Length
            {
                get { return Value.Length; }
            }
        }
    }
}
