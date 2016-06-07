using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {

    public class CobolTokenTag : ITag {

        public CobolTokenTypes TokenType { get; private set; }



        public CobolTokenTag(CobolTokenTypes tokenType) {
            this.TokenType = tokenType;
        }

    }

}
