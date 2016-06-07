
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension.Classification {


    internal sealed class CobolClassifier : ITagger<ClassificationTag> {
        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            throw new NotImplementedException();
        }
    }


}
