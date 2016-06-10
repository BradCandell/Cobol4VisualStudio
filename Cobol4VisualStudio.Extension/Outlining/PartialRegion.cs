
namespace Cobol4VisualStudio.Extension.Outlining {
    public class PartialRegion {

        public int StartLine { get; set; }
        public int StartOffset { get; set; }
        public int Level { get; set; }
        public PartialRegion Parent { get; set; }


    }
}
