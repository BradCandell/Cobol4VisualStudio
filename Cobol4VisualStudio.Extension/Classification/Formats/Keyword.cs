using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classification Format Definition - Cobol Comment
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolKeyword")]
    [Name("cobolKeyword")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolKeywordFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolKeywordFormatDefinition() {
            DisplayName = "Cobol - Keyword";
            ForegroundColor = Colors.Blue;
        }

    }


}
