using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Symbol
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolSymbol")]
    [Name("CobolSymbol")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolSymbolFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolSymbolFormatDefinition() {
            DisplayName = "Cobol - Symbol";
            ForegroundColor = Colors.Black;
        }

    }


}
