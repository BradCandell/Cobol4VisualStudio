using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Cobol Constant
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("cobolConstant")]
        internal static ClassificationTypeDefinition cobolConstant = null;

    }

}
