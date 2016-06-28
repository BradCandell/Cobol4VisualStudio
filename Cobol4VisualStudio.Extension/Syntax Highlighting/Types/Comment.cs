using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Comment
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolComment")]
        internal static ClassificationTypeDefinition cobolComment = null;


    }


}
