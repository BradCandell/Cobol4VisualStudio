using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Cobol Picture Clause
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("cobolPicture")]
        internal static ClassificationTypeDefinition cobolPicture = null;


    }


}
