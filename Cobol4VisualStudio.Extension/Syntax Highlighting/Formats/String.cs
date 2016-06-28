using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - String
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolString")]
    [Name("CobolString")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolStringFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolStringFormatDefinition() {
            DisplayName = "Cobol - String";
            ForegroundColor = Color.FromRgb(0x80, 0x0, 0x0);
        }

    }


}
