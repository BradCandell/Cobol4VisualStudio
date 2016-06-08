using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Tagger Provider - Cobol Classification
    /// </summary>
    [Export(typeof(ITaggerProvider))]
    [ContentType("Cobol")]
    [TagType(typeof(ClassificationTag))]
    internal sealed class CobolClassifierProvider : ITaggerProvider {

        /// <summary>
        /// Content Type Definition
        /// </summary>
        [Export]
        [Name("Cobol")]
        [BaseDefinition("Code")]
        internal static ContentTypeDefinition CobolContentType = null;

        /// <summary>
        /// File Extension to Content Type Definition (CBL)
        /// </summary>
        [Export]
        [FileExtension(".cbl")]
        [ContentType("Cobol")]
        internal static FileExtensionToContentTypeDefinition CobolFileType = null;

        /// <summary>
        /// File Extension to Content Type Definition (CPY)
        /// </summary>
        [Export]
        [FileExtension(".cpy")]
        [ContentType("Cobol")]
        internal static FileExtensionToContentTypeDefinition CopyBookFileType = null;

        /// <summary>
        /// Classification Type Registry
        /// </summary>
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistry = null;

        /// <summary>
        /// Tag Aggregrator Factory Service
        /// </summary>
        [Import]
        internal IBufferTagAggregatorFactoryService AggregatorFactory = null;


        /// <summary>
        /// Create a Cobol Tagger
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="buffer">Text Buffer</param>
        /// <returns>Cobol Tagger</returns>
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {

            ITagAggregator<CobolTokenTag> cobolTagAggregator = AggregatorFactory.CreateTagAggregator<CobolTokenTag>(buffer);
            return new CobolClassifier(buffer, cobolTagAggregator, ClassificationTypeRegistry) as ITagger<T>;

        }


    }

}
