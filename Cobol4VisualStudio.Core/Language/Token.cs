
namespace Cobol4VisualStudio.Core.Language {

    /// <summary>
    /// Token
    /// </summary>
    /// <typeparam name="TToken">The Type of the Token</typeparam>
    public struct Token<TToken> {

        /// <summary>
        /// Get the <see cref="TokenPosition"/> of the <see cref="Token{TToken}"/>.
        /// </summary>
        public TokenPosition Position { get; internal set; }

        /// <summary>
        /// Get the Type of the <see cref="Token{TToken}"/>.
        /// </summary>
        public TToken Type { get; internal set; }

        /// <summary>
        /// Get the Value of the <see cref="Token{TToken}"/>.
        /// </summary>
        public string Value { get; internal set; }

    }

}
