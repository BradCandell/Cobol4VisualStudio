
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {
    public class CobolTokenTagger : ITagger<CobolTokenTag> {

        private ITextBuffer Buffer;

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }


        public CobolTokenTagger(ITextBuffer buffer) {
            this.Buffer = buffer;
        }

        public IEnumerable<ITagSpan<CobolTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            yield break;
        }


    }

}
