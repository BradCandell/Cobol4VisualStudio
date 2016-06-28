using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    [Export(typeof(ITaggerProvider))]
    [ContentType("Cobol")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class CobolSyntaxClassifierProvider : ITaggerProvider {

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService aggregatorFactory = null;


        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {
            ITagAggregator<CobolSyntaxTag> tagAggregator = aggregatorFactory.CreateTagAggregator<CobolSyntaxTag>(buffer);
            return new CobolSyntaxClassifier(buffer, tagAggregator, ClassificationTypeRegistry) as ITagger<T>;
        }

    }

}
