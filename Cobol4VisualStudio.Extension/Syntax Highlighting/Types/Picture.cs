using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Picture Clause
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolPicture")]
        internal static ClassificationTypeDefinition cobolPicture = null;

        /// <summary>
        /// Classification Type Definition - Picture Clause Level
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolPictureLevel")]
        internal static ClassificationTypeDefinition cobolPictureLevel = null;

    }


}
