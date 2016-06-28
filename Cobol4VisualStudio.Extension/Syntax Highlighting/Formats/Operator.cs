using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Operator
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolOperator")]
    [Name("CobolOperator")]
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
