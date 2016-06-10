using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classification Format Definition - Cobol Comment
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolComment")]
    [Name("cobolComment")]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    internal sealed class CobolCommentFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolCommentFormatDefinition() {
            DisplayName = "Cobol - Comment";
            ForegroundColor = Colors.DarkGreen;
        }

    }


}
