using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cobol4VisualStudio.Extension {

    public class CobolSyntaxDefinition {

        public Regex Expression { get; set; }
        public CobolSyntaxTypes Type { get; set; }

    }
}
