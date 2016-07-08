using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {


    /// <summary>
    /// Classification Format Definition - Number
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "CobolNumber")]
    [Name("CobolNumber")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class CobolNumberFormatDefinition : ClassificationFormatDefinition {

        /// <summary>
        /// Default Constructor
        /// </summary>
        public CobolNumberFormatDefinition() {
            DisplayName = "Cobol - Number";
        }

    }



}
