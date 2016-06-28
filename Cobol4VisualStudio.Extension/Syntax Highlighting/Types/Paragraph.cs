using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Paragraph
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolParagraph")]
        internal static ClassificationTypeDefinition cobolParagraph = null;


    }


}
