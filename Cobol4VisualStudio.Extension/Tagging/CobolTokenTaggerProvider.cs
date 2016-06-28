using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension {

    /// <summary>
    /// Cobol Token Tagger Provider
    /// </summary>
    [Export(typeof(ITaggerProvider))]
    [ContentType("CobolOld")]
    [TagType(typeof(CobolTokenTag))]
    public class CobolTokenTaggerProvider : ITaggerProvider {

        /// <summary>
        /// Creates a (Cobol) Tag Provider for the specified buffer
        /// </summary>
        /// <typeparam name="T">Type of Tagger</typeparam>
        /// <param name="buffer">Text Buffer</param>
        /// <returns>Tag Provider</returns>
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {
            return new CobolTokenTagger(buffer) as ITagger<T>;
        }


    }

}
