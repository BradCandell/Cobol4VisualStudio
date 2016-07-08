using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {


    /// <summary>
    /// Classification Format Definition - Section
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolSection")]
    [Name("CobolSection")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolSectionFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolSectionFormatDefinition() {
            DisplayName = "Cobol - Section";
            ForegroundColor = Color.FromRgb(0x99, 0x00, 0x99);
            IsBold = true;
        }

    }



    /// <summary>
    /// Classification Format Definition - Section Name
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolSectionName")]
    [Name("CobolSectionName")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolSectionNameFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolSectionNameFormatDefinition() {
            DisplayName = "Cobol - Section (Name)";
            ForegroundColor = Color.FromRgb(0x99, 0x00, 0x99);
            IsBold = true;
        }

    }



}
