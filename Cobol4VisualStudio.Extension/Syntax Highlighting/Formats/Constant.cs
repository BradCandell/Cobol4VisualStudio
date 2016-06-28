using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Constant
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolConstant")]
    [Name("CobolConstant")]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    internal sealed class CobolConstantFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolConstantFormatDefinition() {
            DisplayName = "Cobol - Constant";
            ForegroundColor = Color.FromRgb(0x87, 0xCE, 0xEB);
        }

    }

}
