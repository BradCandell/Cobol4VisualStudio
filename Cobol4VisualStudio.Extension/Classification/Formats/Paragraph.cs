using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classification Format Definition - Cobol Paragraph
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolParagraph")]
    [Name("cobolParagraph")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolParagraphFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolParagraphFormatDefinition() {
            DisplayName = "Cobol - Paragraph";
            ForegroundColor = Colors.DarkMagenta;
        }

    }


}
