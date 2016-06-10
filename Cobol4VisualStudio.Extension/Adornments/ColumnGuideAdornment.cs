using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Adornments {
    
    /// <summary>
    /// Adornment - Cobol Column Guide
    /// </summary>
    internal sealed class ColumnGuideAdornment {

        /// <summary>
        /// Adornment Layer Definition -  Cobol Column Guide
        /// </summary>
        [Export]
        [Name("CobolColumnGuideAdornment")]
        [Order(After = PredefinedAdornmentLayers.Selection, Before = PredefinedAdornmentLayers.Text)]
        internal AdornmentLayerDefinition columnGuideAdornmentLayerDefinition;

    }

}
