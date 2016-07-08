using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {

    internal sealed class CobolSyntaxClassifier : ITagger<ClassificationTag> {

        ITextBuffer _buffer;
        ITagAggregator<CobolSyntaxTag> _aggregator;
        IDictionary<CobolSyntaxTypes, IClassificationType> _tokenTypes;


        
        internal CobolSyntaxClassifier(ITextBuffer buffer, ITagAggregator<CobolSyntaxTag> aggregator, IClassificationTypeRegistryService typeService) {
            _buffer = buffer;
            _aggregator = aggregator;
            _tokenTypes = new Dictionary<CobolSyntaxTypes, IClassificationType>() {
                { CobolSyntaxTypes.Comment, typeService.GetClassificationType("CobolComment") },
                { CobolSyntaxTypes.Constant, typeService.GetClassificationType("CobolConstant") },
                { CobolSyntaxTypes.Division, typeService.GetClassificationType("CobolDivision") },
                { CobolSyntaxTypes.DivisionName, typeService.GetClassificationType("CobolDivisionName") },
                { CobolSyntaxTypes.Identifier, typeService.GetClassificationType("CobolIdentifier") },
                { CobolSyntaxTypes.Keyword, typeService.GetClassificationType("CobolKeyword") },
                { CobolSyntaxTypes.Number, typeService.GetClassificationType("CobolNumber") },
                { CobolSyntaxTypes.Operator, typeService.GetClassificationType("CobolOperator") },
                { CobolSyntaxTypes.Paragraph, typeService.GetClassificationType("CobolParagraph") },
                { CobolSyntaxTypes.Picture, typeService.GetClassificationType("CobolPicture") },
                { CobolSyntaxTypes.PictureLevel, typeService.GetClassificationType("CobolPictureLevel") },
                { CobolSyntaxTypes.Section, typeService.GetClassificationType("CobolSection") },
                { CobolSyntaxTypes.SectionName, typeService.GetClassificationType("CobolSectionName") },
                { CobolSyntaxTypes.SequenceNumber, typeService.GetClassificationType("CobolSequenceNumber") },
                { CobolSyntaxTypes.String, typeService.GetClassificationType("CobolString") },
                { CobolSyntaxTypes.Symbol, typeService.GetClassificationType("CobolSymbol") }
            };
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            foreach (var span in _aggregator.GetTags(spans)) {
                var tagspans = span.Span.GetSpans(spans[0].Snapshot);
                yield return new TagSpan<ClassificationTag>(tagspans[0], new ClassificationTag(_tokenTypes[span.Tag.Type]));
            }
        }
    }
}
