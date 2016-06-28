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


}
