using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Classification {


    /// <summary>
    /// Classification Format Definition - Cobol Section
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "cobolSection")]
    [Name("cobolSection")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolSectionFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolSectionFormatDefinition() {
            DisplayName = "Cobol - Section";
            ForegroundColor = Color.FromRgb(0xBA, 0x55, 0xD3);
            IsBold = true;
        }

    }



}
