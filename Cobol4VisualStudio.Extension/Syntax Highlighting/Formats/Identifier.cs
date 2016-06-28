using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {


    /// <summary>
    /// Classification Format Definition - Identifier
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolIdentifier")]
    [Name("CobolIdentifier")]
    [UserVisible(true)]
    [Order(Before = Priority.Low)]
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
