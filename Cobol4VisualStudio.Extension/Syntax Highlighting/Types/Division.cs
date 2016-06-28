using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Internal - Cobol Classification Types
    /// </summary>
    internal static partial class CobolClassificationTypes {

        /// <summary>
        /// Classification Type Definition - Division
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolDivision")]
        internal static ClassificationTypeDefinition cobolDivision = null;

        /// <summary>
        /// Classification Type Definition - Division Name
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("CobolDivisionName")]
        internal static ClassificationTypeDefinition cobolDivisionName = null;

    }


}
