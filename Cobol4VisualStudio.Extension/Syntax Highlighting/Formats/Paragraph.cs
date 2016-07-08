using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Paragraph
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolParagraph")]
    [Name("CobolParagraph")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolParagraphFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolParagraphFormatDefinition() {
            DisplayName = "Cobol - Paragraph";
            ForegroundColor = Color.FromRgb(0xCC, 0x00, 0xCC);
        }

    }


}
