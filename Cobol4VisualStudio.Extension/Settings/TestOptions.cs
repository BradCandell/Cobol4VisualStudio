﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell;

namespace Cobol4VisualStudio.Extension.Settings {
    
    
    public class MyOptionsGrid : DialogPage {

        [Category("General")]
        [DisplayName("Show editor margin")]
        [Description("The editor margin shows information about the encoding, textbuffers and classifications.")]
        [DefaultValue(true)]
        public bool ShowBottomMargin { get; set; } = true;

        [Category("Pkgdef editor")]
        [DisplayName("Enable Intellisense")]
        [Description("Any open .pkgdef files must be reopened for the setting to take effect.")]
        [DefaultValue(true)]
        public bool PkgdefShowIntellisense { get; set; } = true;

        [Category("Pkgdef editor")]
        [DisplayName("Enable validation")]
        [Description("Validation of unclosed registry keys and string and more.")]
        [DefaultValue(true)]
        public bool PkgdefEnableValidation { get; set; } = true;

        [Category("Pkgdef editor")]
        [DisplayName("Enable colorization")]
        [Description("Classifies .pkgdef files and adds syntax highlighting.")]
        [DefaultValue(true)]
        public bool PkgdefEnableColorizer { get; set; } = true;

        [Category("Pkgdef editor")]
        [DisplayName("Enable outlining")]
        [Description("Adds outlining (code folding) to registry keys in .pkgdef files.")]
        [DefaultValue(true)]
        public bool PkgdefEnableOutlining { get; set; } = true;

        [Category("Pkgdef editor")]
        [DisplayName("Enable brace matching")]
        [Description("Highlights the matching start- or end brace, paranthesis or quotation mark.")]
        [DefaultValue(true)]
        public bool PkgdefEnableBraceMatching { get; set; } = true;

        [Category("VSCT files")]
        [DisplayName("Enable Intellisense")]
        [Description("Gives Intellisense for 'guid' and 'id' attributes.")]
        [DefaultValue(true)]
        public bool VsctEnableIntellisense { get; set; } = true;

        protected override void OnApply(PageApplyEventArgs e) {
            if (!PkgdefEnableColorizer) {
                PkgdefShowIntellisense = false;
            }

            base.OnApply(e);
        }

    }
}
