
using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    [Export(typeof(ITaggerProvider))]
    [ContentType("Cobol")]
    [TagType(typeof(CobolTokenTag))]
    public class CobolTokenTaggerProvider : ITaggerProvider {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {
            return new CobolTokenTagger(buffer) as ITagger<T>;
        }
    }

}
