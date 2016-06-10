using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    /// <summary>
    /// Classification Format Definition - Cobol Constant
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolConstant")]
    [Name("cobolConstant")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
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
