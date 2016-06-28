using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {


    /// <summary>
    /// Classification Format Definition - Sequence/Line Number
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolSequenceNumber")]
    [Name("CobolSequenceNumber")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolLineNumberFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolLineNumberFormatDefinition() {
            DisplayName = "Cobol - Sequence/Line Number";
            ForegroundColor = Color.FromRgb(195, 195, 195);
        }

    }

}
