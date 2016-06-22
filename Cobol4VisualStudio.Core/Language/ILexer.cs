using System.Collections.Generic;

namespace Cobol4VisualStudio.Core.Language {

    /// <summary>
    /// Interface - Lexer
    /// </summary>
    /// <typeparam name="TToken">Type of Token that the Implemented Lexer can Tokenize</typeparam>
    public interface ILexer<TToken> {

        /// <summary>
        /// Gets the <see cref="IGrammar{TToken}"/> of this Lexer.
        /// </summary>
        IGrammar<TToken> Grammar { get; }

        /// <summary>
        /// Tokenize the specified string into a collection of <see cref="Token{TToken}"/> results.
        /// </summary>
        /// <param name="input">Input String to be tokenized</param>
        /// <returns>Collection of <see cref="Token{TToken}"/> results</returns>
        IEnumerable<Token<TToken>> Tokenize(string input);

    }

}
