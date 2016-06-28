using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    [Export(typeof(ITaggerProvider))]
    [ContentType("Cobol")]
    [TagType(typeof(CobolSyntaxTag))]
    internal sealed class CobolSyntaxHighlighterProvider : ITaggerProvider {

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {
            return new CobolSyntaxHighlighter(buffer) as ITagger<T>;
        }

    }
}
