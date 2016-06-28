using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Section
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolSection")]
        internal static ClassificationTypeDefinition cobolSection = null;

        /// <summary>
        /// Classification Type Definition - Section Name
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolSectionName")]
        internal static ClassificationTypeDefinition cobolSectionName = null;

    }


}
