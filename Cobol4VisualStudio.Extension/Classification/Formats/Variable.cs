using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {

    /// <summary>
    /// Classification Format Definition - Cobol Variable
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolVariable")]
    [Name("cobolVariable")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolVariableFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolVariableFormatDefinition() {
            DisplayName = "Cobol - Variable";
            ForegroundColor = Colors.DarkTurquoise;
        }

    }


}
