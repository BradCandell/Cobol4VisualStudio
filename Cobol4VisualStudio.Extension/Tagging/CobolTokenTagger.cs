
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {


    internal sealed class CobolTokenTagger : ITagger<CobolTokenTag> {

        ITextBuffer _buffer;


        private static IDictionary<CobolTokenTypes, List<Regex>> tokenParsers = new Dictionary<CobolTokenTypes, List<Regex>>() {
            { CobolTokenTypes.Comment, new List<Regex>() { new Regex(@"(\*>.*$|(?<=[\d]{6})\*.*)") } },
            { CobolTokenTypes.Division, new List<Regex>() { new Regex(@"\b(IDENTIFICATION|ENVIRONMENT|DATA|PROCEDURE)\sDIVISION\.") } },
            { CobolTokenTypes.LineNumber, new List<Regex>() { new Regex(@"^[\d]{6}") } }
        };



        public CobolTokenTagger(ITextBuffer buffer) {
            _buffer = buffer;
        }


        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }


        public IEnumerable<ITagSpan<CobolTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans) {

            if (spans.Count == 0 || spans[0].Length == 0 || spans[0].Length >= _buffer.CurrentSnapshot.Length) {
                yield break;
            }


            foreach (SnapshotSpan span in spans) {


                ITextSnapshotLine containingLine = span.Start.GetContainingLine();
                int currentLocation = containingLine.Start.Position;

                string text = containingLine.GetText();

                foreach (var key in tokenParsers.Keys) {
                    foreach(Regex r in tokenParsers[key]) {
                        if (r.IsMatch(text)) {
                            Match m = r.Match(text);
                            yield return new TagSpan<CobolTokenTag>(new SnapshotSpan(span.Snapshot, currentLocation + m.Index, m.Length), new CobolTokenTag(key));
                        }
                    }
                }

            }

            yield break;

        }
    }

}