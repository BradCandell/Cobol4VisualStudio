
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
            { CobolTokenTypes.LineNumber, new List<Regex>() { new Regex(@"^[\d]{6}") } },
            { CobolTokenTypes.Keyword, new List<Regex>() { new Regex(@"\b(((END-)?(ACCEPT|ADD|CALL|COMPUTE|DELETE|DISPLAY|DIVIDE|EVALUATE|IF|MULTIPLY|PERFORM|READ|RECEIVE|RETURN|REWRITE|SEARCH|START|STRING|SUBTRACT|UNSTRING|WRITE))|ALTER|ASSIGN|CHAIN|CLOSE|CONTINUE|CONTROL|COPY|COUNT|ELSE|ENABLE|ERASE|EXIT|GENERATE|GO|GOBACK|IGNORE|INITIALIZE|INITIATE|INSPECT|INVOKE|MERGE|MOVE|OPEN|RELEASE|REPLACE|RESERVE|RESET|REWIND|ROLLBACK|RUN|SELECT|SEND|SET|SORT|STOP|SUM|SUPPRESS|TERMINATE|THEN|TRANSFORM|UNLOCK|UPDATE|USE|WAIT|WHEN)\b(?!-)") } },
            { CobolTokenTypes.Picture, new List<Regex>() {
                new Regex(@"(?i:picture\s+is|picture|pic\s+is|pic)\s+[-+sS\*$09aAbBxXpPnNzZ/,.]*\([0-9]*\)[vV\.][-+s\*$09aAbBsSnNxXzZ/,]*\([0-9]*\)"),
                new Regex(@"(?i:picture\s+is|picture|pic\s+is|pic)\s+[-+sS\*$09aAbBsSnpPNxXzZ/,.]*\([0-9]*\)[Vv\.][-+s\*0$9aAbBsSnNxpPXzZ/,]*")
            } }
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