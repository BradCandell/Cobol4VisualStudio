using System;
using System.Text.RegularExpressions;

namespace Cobol4VisualStudio.Core.Language {

    /// <summary>
    /// Token Definition / Rule
    /// </summary>
    /// <typeparam name="TToken">Type of Token that the <see cref="TokenDefinition{TToken}"/> is capable of resolving.</typeparam>
    public class TokenDefinition<TToken> {

        /// <summary>
        /// Get the <see cref="Divisions"/> that the <see cref="TokenDefinition{TToken}"/> should tokenize
        /// </summary>
        public Divisions Divisions { get; internal set; }

        /// <summary>
        /// Get the Regular Expression (<see cref="Regex"/>) that will match the specified Token Type (<see cref="TToken"/>).
        /// </summary>
        public Regex Expression { get; internal set; }

        /// <summary>
        /// Get whether this <see cref="TokenDefinition{TToken}"/> should be ignored when tokenizing.
        /// </summary>
        public bool IsIgnored { get; internal set; }

        /// <summary>
        /// Get the Type of Token that this <see cref="TokenDefinition{TToken}"/> can resolve to when tokenizing.
        /// </summary>
        public TToken Type { get; internal set; }



                        
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="type">The Type of Token that this <see cref="TokenDefinition{TToken}"/> can resolve.</param>
        /// <param name="expression">The Regular Expression (<see cref="Regex"/>) that will match the specified Token Type (<see cref="TToken"/>).</param>
        public TokenDefinition(TToken type, Regex expression) : this(type, expression, false) {

        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="type">The Type of Token that this <see cref="TokenDefinition{TToken}"/> can resolve.</param>
        /// <param name="expression">The Regular Expression (<see cref="Regex"/>) that will match the specified Token Type (<see cref="TToken"/>).</param>
        /// <param name="divisions">The <see cref="Divisions"/> that the <see cref="TokenDefinition{TToken}"/> should tokenize</param>
        public TokenDefinition(TToken type, Regex expression, Divisions divisions) : this(type, expression, divisions, false) {
                
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="type">The Type of Token that this <see cref="TokenDefinition{TToken}"/> can resolve.</param>
        /// <param name="expression">The Regular Expression (<see cref="Regex"/>) that will match the specified Token Type (<see cref="TToken"/>).</param>
        /// <param name="isIgnored">Specifies whether this <see cref="TokenDefinition{TToken}"/> should be ignored when tokenizing.</param>
        public TokenDefinition(TToken type, Regex expression, bool isIgnored) : this(type, expression, Divisions.All, isIgnored) {

        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="type">The Type of Token that this <see cref="TokenDefinition{TToken}"/> can resolve.</param>
        /// <param name="expression">The Regular Expression (<see cref="Regex"/>) that will match the specified Token Type (<see cref="TToken"/>).</param>
        /// <param name="divisions">The <see cref="Divisions"/> that the <see cref="TokenDefinition{TToken}"/> should tokenize</param>
        /// <param name="isIgnored">Specifies whether this <see cref="TokenDefinition{TToken}"/> should be ignored when tokenizing.</param>
        public TokenDefinition(TToken type, Regex expression, Divisions divisions, bool isIgnored) {
            this.Type = type;
            this.Expression = expression;
            this.Divisions = divisions;
            this.IsIgnored = isIgnored;
        }




    }
}
