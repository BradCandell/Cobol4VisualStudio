using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Tag - Cobol Token
    /// </summary>
    public class CobolTokenTag : ITag {

        /// <summary>
        /// Get the Type of this Cobol Token
        /// </summary>
        public CobolTokenTypes TokenType { get; private set; }




        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="tokenType">Tag Token Type</param>
        public CobolTokenTag(CobolTokenTypes tokenType) {
            this.TokenType = tokenType;
        }


    }
}
