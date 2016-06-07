using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classification Format Definition - Cobol Comment
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolPicture")]
    [Name("cobolPicture")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolPictureFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolPictureFormatDefinition() {
            DisplayName = "Cobol - Picture";
            ForegroundColor = Colors.DarkOrange;
        }

    }


}
