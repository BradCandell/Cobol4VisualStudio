using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {

    
    public class CobolSyntaxTag : ITag {

        public CobolSyntaxTypes Type { get; private set; }

        public CobolSyntaxTag(CobolSyntaxTypes type) {
            Type = type;
        }

    }

}
