using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Comment
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolComment")]
    [Name("CobolComment")]
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
