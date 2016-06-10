using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    /// <summary>
    /// Classification Format Definition - Cobol Number
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolNumber")]
    [Name("cobolNumber")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolNumberFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolNumberFormatDefinition() {
            DisplayName = "Cobol - Number";
            ForegroundColor = Colors.Black;
        }

    }



}
