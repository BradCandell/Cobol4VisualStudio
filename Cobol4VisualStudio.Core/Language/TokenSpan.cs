using System;


namespace Cobol4VisualStudio.Core.Language {

    /// <summary>
    /// Token Span - Specifies the Start and End location of a given <see cref="Token{TToken}"/>
    /// </summary>
    public struct TokenSpan {
        
        /// <summary>
        /// Get the Start Index of the <see cref="Token{TToken}"/>.
        /// </summary>
        public int Start { get; internal set; }

        /// <summary>
        /// Get the End Index of the <see cref="Token{TToken}"/>.
        /// </summary>
        public int End { get; internal set; }

        /// <summary>
        /// Get the Length of the <see cref="Token{TToken}"/>.
        /// </summary>
        public int Length {
            get {
                return End - Start;
            }
        }



        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="start">The Start Index of the <see cref="Token{TToken}"/>.</param>
        /// <param name="end">The End Index of the <see cref="Token{TToken}"/>.</param>
        public TokenSpan(int start, int end) {

            if (end <= start) {
                throw new ArgumentOutOfRangeException("end", end, string.Format("The Ending value ({1}) cannot be less than, or equal to the Starting Value ({0}).", start, end));
            }

            this.Start = start;
            this.End = end;

        }

        
    }

}
