using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    /// <summary>
    /// Classification Format Definition - Cobol Division
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolDivision")]
    [Name("cobolDivision")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolDivisionFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolDivisionFormatDefinition() {
            DisplayName = "Cobol - Division";
            ForegroundColor = Colors.Purple;
        }

    }



}
