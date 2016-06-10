
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Adornments {
    
    internal sealed class ColumnGuideAdornment {

        [Export]
        [Name("CobolColumnGuideAdornment")]
        [Order(After = PredefinedAdornmentLayers.Selection, Before = PredefinedAdornmentLayers.Text)]
        internal AdornmentLayerDefinition columnGuideAdornmentLayerDefinition;

    }

}
