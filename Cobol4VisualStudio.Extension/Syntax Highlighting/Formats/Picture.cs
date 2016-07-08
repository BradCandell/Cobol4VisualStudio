using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Picture Clause
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolPicture")]
    [Name("CobolPicture")]
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

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolPictureLevel")]
    [Name("CobolPictureLevel")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolPictureLevelFormatDefinition: ClassificationFormatDefinition {

        public CobolPictureLevelFormatDefinition() {
            DisplayName = "Cobol - Picture Level";
            ForegroundColor = Color.FromRgb(0x99, 0x4c, 0x00);
        }
    }

}
