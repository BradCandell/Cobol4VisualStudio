using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    /// <summary>
    /// Classification Format Definition - Cobol Identifier
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolIdentifier")]
    [Name("cobolIdentifier")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolIdentifierFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolIdentifierFormatDefinition() {
            DisplayName = "Cobol - Identifier";
            ForegroundColor = Color.FromRgb(0x43, 0x91, 0xAF);
        }

    }



}
