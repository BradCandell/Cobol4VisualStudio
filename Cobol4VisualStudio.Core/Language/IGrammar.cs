using System.Collections.Generic;

namespace Cobol4VisualStudio.Core.Language {

    /// <summary>
    /// Interface - Language Grammar (Set of Rules that define the specified Token Types)
    /// </summary>
    /// <typeparam name="TToken">The Type of the Token that the Implemented Grammar defines</typeparam>
    public interface IGrammar<TToken> {

        /// <summary>
        /// Get the Name of the Grammar
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Get the Collection of <see cref="TokenDefinition{TToken}"/> that represent this Grammar.
        /// </summary>
        IEnumerable<TokenDefinition<TToken>> Definitions { get; }

    }
}
