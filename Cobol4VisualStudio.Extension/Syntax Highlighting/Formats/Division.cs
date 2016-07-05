using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Classification Format Definition - Division
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolDivision")]
    [Name("CobolDivision")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class CobolDivisionFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolDivisionFormatDefinition() {
            DisplayName = "Cobol - Division";
            ForegroundColor = Colors.Purple;
            IsBold = true;
        }

    }



    /// <summary>
    /// Classification Format Definition - Division Name
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolDivisionName")]
    [Name("CobolDivisionName")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class CobolDivisionNameFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolDivisionNameFormatDefinition() {
            DisplayName = "Cobol - Division (Name)";
            ForegroundColor = Colors.Purple;
            IsBold = true;
        }

    }


}
