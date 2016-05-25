using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    /// <summary>
    /// Classification Format Definition - Cobol Line Number
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolLineNumber")]
    [Name("cobolLineNumber")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolLineNumberFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolLineNumberFormatDefinition() {
            DisplayName = "Cobol - Line Number";
            ForegroundColor = Color.FromRgb(195, 195, 195);
        }

    }

}
