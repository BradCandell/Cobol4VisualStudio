using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobol4VisualStudio.Extension.Outlining {

    public enum CobolOutliningRegionType {
        Division,
        Section,
        Paragraph
    }

    public class CobolOutliningRegion {

        public int Start { get; set; }
        public int StartLine { get; set; }
        public int StartOffset { get; set; }
        public int End { get; set; }
        public int EndLine { get; set; }
        public CobolOutliningRegionType RegionType { get; set; }
        public string Text { get; set; }
        public string CollapsedText { get; set; }

    }
}
