using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    [Export(typeof(ITaggerProvider))]
    [ContentType("Cobol")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class CobolClassifierProvider : ITaggerProvider {

        [Export]
        [Name("Cobol")]
        [BaseDefinition("Code")]
        internal static ContentTypeDefinition CobolContentType = null;

        [Export]
        [FileExtension(".cbl")]
        [ContentType("Cobol")]
        internal static FileExtensionToContentTypeDefinition CobolFileType = null;

        [Export]
        [FileExtension(".cpy")]
        [ContentType("Cobol")]
        internal static FileExtensionToContentTypeDefinition CopyBookFileType = null;

        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        [Import]
        internal IBufferTagAggregatorFactoryService AggregatorFactory = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {

            ITagAggregator<CobolTokenTag> cobolTagAggregator = AggregatorFactory.CreateTagAggregator<CobolTokenTag>(buffer);
            return new CobolClassifier(buffer, cobolTagAggregator, ClassificationTypeRegistry) as ITagger<T>;

        }


    }

}
