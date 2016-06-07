using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classification Format Definition - Cobol Comment
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolOperator")]
    [Name("cobolOperator")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolOperatorFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolOperatorFormatDefinition() {
            DisplayName = "Cobol - Operator";
            ForegroundColor = Colors.Aqua;
        }

    }


}
