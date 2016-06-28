using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Cobol Type Classification
    /// </summary>
    [ContentType("Cobol")]
    internal sealed class CobolClassification {

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

    }
}
