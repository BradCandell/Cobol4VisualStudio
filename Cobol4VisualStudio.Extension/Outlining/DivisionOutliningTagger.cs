using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Outlining;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace Cobol4VisualStudio.Extension.Outlining {

    [Export(typeof(ITaggerProvider))]
    [TagType(typeof(IOutliningRegionTag))]
    [ContentType("Cobol")]
    internal sealed class OutliningTaggerProvider : ITaggerProvider {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag {

            //create a single tagger for each buffer.
            Func<ITagger<T>> sc = delegate () { return new DivisionOutliningTagger(buffer) as ITagger<T>; };
            return buffer.Properties.GetOrCreateSingletonProperty<ITagger<T>>(sc);

        }

    }


    internal sealed class DivisionOutliningTagger : ITagger<IOutliningRegionTag> {

        ITextBuffer buffer;
        ITextSnapshot snapshot;
        List<Region> regions;
        private string currentDivision = null;
        private static Regex divisionRegex = new Regex(@"\b(IDENTIFICATION|ID|ENVIRONMENT|DATA|PROCEDURE)[\s]+DIVISION\.", RegexOptions.IgnoreCase);
        private static Regex sectionRegex = new Regex(@"(^|[\t\s])\b(?<![-])([\w\d-]+[\w\d][\s]+)SECTION\.", RegexOptions.IgnoreCase);

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;


        public DivisionOutliningTagger(ITextBuffer textBuffer) {
            this.buffer = textBuffer;
            this.snapshot = textBuffer.CurrentSnapshot;
            this.regions = new List<Region>();
            //this.ParseRegions();
            this.buffer.Changed += Buffer_Changed;
        }

        private void Buffer_Changed(object sender, TextContentChangedEventArgs e) {
            
        }

        public IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            
            if (spans == null || spans.Count == 0) {
                yield break;
            }

            //List<Region> currentRegions = this.regions;
            ITextSnapshot currentSnapshot = this.snapshot;
            SnapshotSpan entire = new SnapshotSpan(spans[0].Start, spans[spans.Count - 1].End).TranslateTo(currentSnapshot, SpanTrackingMode.EdgeExclusive);
            int startLineNumber = entire.Start.GetContainingLine().LineNumber;
            int endLineNumber = entire.End.GetContainingLine().LineNumber;

            string tDivision = "";
            string tSection = "";
            string tParagraph = "";

            foreach (var line in currentSnapshot.Lines) {
                string text = line.GetText();    

                if (divisionRegex.IsMatch(text)) {
                    tDivision = divisionRegex.Match(text).Groups[0].Value;
                    tSection = "";
                    tParagraph = "";
                }

                if (sectionRegex.IsMatch(text)) {
                    tSection = sectionRegex.Match(text).Groups[2].Value;
                    tParagraph = "";
                }

                System.Diagnostics.Debug.WriteLine("[" + line.LineNumber.ToString().PadLeft(3, '0') + "]  " + tDivision + " | " + tSection + " | " + tParagraph + "   [ " + text + " ]");
            }

            //string divisionName = "";
            //int startLineOfDivision = -1;
            //int lastLineOfDivision = -1;

            //for (int i = 0; i <= endLineNumber; i++) {
            //    currentSnapshot.GetLineFromLineNumber(i).GetText();
            //}
            //foreach (var line in currentSnapshot.Lines) {
            //    string text = line.GetText();

            //    if (divisionRegex.IsMatch(text)) {
            //        Match m = divisionRegex.Match(text);
            //        divisionName = m.Value;


            //    }
            //}

            //System.Diagnostics.Debug.WriteLine("Brad");
            //foreach (var region in currentRegions) {
            //    if (region.StartLine <= endLineNumber &&
            //        region.EndLine >= startLineNumber) {
            //        var startLine = currentSnapshot.GetLineFromLineNumber(region.StartLine);
            //        var endLine = currentSnapshot.GetLineFromLineNumber(region.EndLine);

            //        //the region starts at the beginning of the "[", and goes until the *end* of the line that contains the "]".
            //        //yield return new TagSpan<IOutliningRegionTag>(
            //            //new SnapshotSpan(startLine.Start + region.StartOffset,
            //            //endLine.End),
            //            //new OutliningRegionTag(false, false, ellipsis, hoverText));
            //    }
            //}


            //yield return new TagSpan<IOutliningRegionTag>(new SnapshotSpan(new SnapshotPoint(snapshot, 7), new SnapshotPoint(snapshot, 195)), new OutliningRegionTag(false, false, "BRAD", "THIS IS THE ID DIVISION"));

            string currentDivision = null;
            int currentDivisionStart = -1;

            for (int i = startLineNumber; i <= endLineNumber; i++) {

                var line = currentSnapshot.GetLineFromLineNumber(i);
                string text = line.GetText();

                if (divisionRegex.IsMatch(text) || i == endLineNumber) {
                    Match m = divisionRegex.Match(text);

                    if ((currentDivision != null && currentDivisionStart > -1)) {
                        //System.Diagnostics.Debug.WriteLine("Division Region is Complete [" + currentDivision + "] from " + currentDivisionStart + " to " + currentSnapshot.GetLineFromLineNumber(i - 1).End.Position);
                        yield return new TagSpan<IOutliningRegionTag>(new SnapshotSpan(new SnapshotPoint(snapshot, currentDivisionStart), currentSnapshot.GetLineFromLineNumber(i - 1).End), new OutliningRegionTag(false, false, currentDivision, "THIS IS THE ID DIVISION"));
                    }

                    if ((currentDivision != null && i == endLineNumber)) {
                        //System.Diagnostics.Debug.WriteLine("Division Region is Complete [" + currentDivision + "] from " + currentDivisionStart + " to " + currentSnapshot.GetLineFromLineNumber(i - 1).End.Position);
                        yield return new TagSpan<IOutliningRegionTag>(new SnapshotSpan(new SnapshotPoint(snapshot, currentDivisionStart), currentSnapshot.GetLineFromLineNumber(i).End), new OutliningRegionTag(false, false, currentDivision, "THIS IS THE ID DIVISION"));
                    }
                    currentDivision = m.Value;
                    currentDivisionStart = line.Start.Position + m.Index;

                    //System.Diagnostics.Debug.WriteLine("Division Found [" + currentDivision + "] at " + currentDivisionStart);
                }
            }

        }
    }
}
