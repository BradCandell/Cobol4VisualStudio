using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classifier - Cobol
    /// </summary>
    internal sealed class CobolClassifier : ITagger<ClassificationTag> {

        private ITextBuffer Buffer;
        private ITagAggregator<CobolTokenTag> Aggregator;
        private IDictionary<CobolTokenTypes, IClassificationType> CobolTypes;



        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="buffer">Text Buffer</param>
        /// <param name="cobolTagAggregator">Cobol Tag Aggregator</param>
        /// <param name="typeService">Classification Type Registry Service</param>
        public CobolClassifier(ITextBuffer buffer, ITagAggregator<CobolTokenTag> cobolTagAggregator, IClassificationTypeRegistryService typeService) {
            Buffer = buffer;
            Aggregator = cobolTagAggregator;
            CobolTypes = new Dictionary<CobolTokenTypes, IClassificationType>();
            CobolTypes[CobolTokenTypes.Comment] = typeService.GetClassificationType("cobolComment");
            CobolTypes[CobolTokenTypes.Constant] = typeService.GetClassificationType("cobolConstant");
            CobolTypes[CobolTokenTypes.Division] = typeService.GetClassificationType("cobolDivision");
            CobolTypes[CobolTokenTypes.Identifier] = typeService.GetClassificationType("cobolIdentifier");
            CobolTypes[CobolTokenTypes.Keyword] = typeService.GetClassificationType("cobolKeyword");
            CobolTypes[CobolTokenTypes.LineNumber] = typeService.GetClassificationType("cobolLineNumber");
            CobolTypes[CobolTokenTypes.Number] = typeService.GetClassificationType("cobolNumber");
            CobolTypes[CobolTokenTypes.Operator] = typeService.GetClassificationType("cobolOperator");
            CobolTypes[CobolTokenTypes.Paragraph] = typeService.GetClassificationType("cobolParagraph");
            CobolTypes[CobolTokenTypes.Picture] = typeService.GetClassificationType("cobolPicture");
            CobolTypes[CobolTokenTypes.Section] = typeService.GetClassificationType("cobolSection");
            CobolTypes[CobolTokenTypes.String] = typeService.GetClassificationType("cobolString");
            CobolTypes[CobolTokenTypes.Variable] = typeService.GetClassificationType("cobolVariable");
        }


        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<ClassificationTag>> GetTags(NormalizedSnapshotSpanCollection spans) {

            foreach (var span in Aggregator.GetTags(spans)) {
                var tagSpans = span.Span.GetSpans(spans[0].Snapshot);
                yield return new TagSpan<ClassificationTag>(tagSpans[0], new ClassificationTag(CobolTypes[span.Tag.TokenType]));
            }

        }

    }


}
