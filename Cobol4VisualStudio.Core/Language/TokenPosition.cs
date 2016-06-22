
namespace Cobol4VisualStudio.Core.Language {

    /// <summary>
    /// Describes the Position of a <see cref="Token{TToken}"/>
    /// </summary>
    public struct TokenPosition {

        /// <summary>
        /// Get the the Cobol Division that the <see cref="Token{TToken}"/> was found.
        /// </summary>
        public Divisions Division { get; internal set; }

        /// <summary>
        /// Get the Cobol Section that the <see cref="Token{TToken}"/> was found.
        /// </summary>
        public string Section { get; internal set; }

        /// <summary>
        /// Get the Cobol Paragraph that the <see cref="Token{TToken}"/> was found.
        /// </summary>
        public string Paragraph { get; internal set; }

        /// <summary>
        /// Get the Line Number of the <see cref="Token{TToken}"/> that was found.
        /// </summary>
        public int Line { get; internal set; }

        /// <summary>
        /// Get the Length of the <see cref="Token{TToken}"/> that was found.
        /// </summary>
        public int Length { get; internal set; }

        /// <summary>
        /// Get the <see cref="TokenSpan"/> of the <see cref="Token{TToken}"/> that was found, as it is indexed in the entire source code.
        /// </summary>
        public TokenSpan Span { get; internal set; }

        /// <summary>
        /// Get the <see cref="TokenSpan"/> of the <see cref="Token{TToken}"/> that was found, relative to its line.
        /// </summary>
        public TokenSpan LineSpan { get; internal set; }
        
    }

}
