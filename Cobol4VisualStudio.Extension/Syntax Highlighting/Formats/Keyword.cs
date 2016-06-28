using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Keyword / Reserved Word
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolKeyword")]
    [Name("CobolKeyword")]
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
