
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension.Outlining {

    internal sealed class CobolOutliningTagger : ITagger<IOutliningRegionTag> {

        ITextBuffer buffer;
        ITextSnapshot snapshot;
        List<CobolOutliningRegion> regions;
        private static Regex parsingExpression = new Regex(@"\b((?<name>IDENTIFICATION|ID|ENVIRONMENT|DATA|PROCEDURE)[\s]+DIVISION(?=\.|[\s]??(USING|GIVING|RETURNING))|(?<name>[A-Za-z0-9-]+)[\s]+SECTION\.|(?<=(^[\d]{6}\s{1,4}))(?<![-])(?=[A-Za-z0-9-]*?[A-Z])(?<![-])\b(?<name>[A-Za-z0-9-]{1,30})\b(?!-)(?=\.))", RegexOptions.IgnoreCase);

        private SnapshotSpan AsSnapshotSpan(CobolOutliningRegion region, ITextSnapshot snapshot) {
            var startLine = snapshot.GetLineFromLineNumber(region.StartLine);
            var endLine = (region.StartLine == region.EndLine) ? startLine
                 : snapshot.GetLineFromLineNumber(region.EndLine);
            return new SnapshotSpan(startLine.Start + region.StartOffset, endLine.End);
        }


        public CobolOutliningTagger(ITextBuffer buffer) {
            this.buffer = buffer;
            this.snapshot = buffer.CurrentSnapshot;
            this.regions = new List<Outlining.CobolOutliningRegion>();
            this.Reparse();
            this.buffer.Changed += Buffer_Changed;
        }

        private void Buffer_Changed(object sender, TextContentChangedEventArgs e) {
            if (e.After != buffer.CurrentSnapshot) {
                return;
            }

            this.Reparse();

        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        public IEnumerable<ITagSpan<IOutliningRegionTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            
            if (spans.Count == 0) { yield break; }

            List<CobolOutliningRegion> currentRegions = this.regions;
            ITextSnapshot currentSnapshot = this.snapshot;
            SnapshotSpan entire = new SnapshotSpan(spans[0].Start, spans[spans.Count - 1].End).TranslateTo(currentSnapshot, SpanTrackingMode.EdgeExclusive);

            int startLineNumber = entire.Start.GetContainingLine().LineNumber;
            int endLineNumber = entire.End.GetContainingLine().LineNumber;

            System.Diagnostics.Debug.WriteLine("{0} thru {1}", startLineNumber, endLineNumber);

            foreach (var region in currentRegions) {
                if (region.StartLine <= endLineNumber && region.EndLine >= startLineNumber) {
                    var startLine = currentSnapshot.GetLineFromLineNumber(region.StartLine);
                    var endLine = currentSnapshot.GetLineFromLineNumber(region.EndLine);

                    yield return new TagSpan<IOutliningRegionTag>(
                        new SnapshotSpan(startLine.Start + region.StartOffset, endLine.End),
                        new OutliningRegionTag(false, false, region.CollapsedText, "..."));
                    //yield return new TagSpan<IOutliningRegionTag>(new SnapshotSpan(new SnapshotPoint(snapshot, region.Start), region.End - region.Start), new OutliningRegionTag(false, false, region.Text, "..."));
                }
                
                //if (region.Start <= endLineNumber && region.End >= startLineNumber) {
                    //var startLine = currentSnapshot.GetLineFromLineNumber(region.Start);
                    //var endLine = currentSnapshot.GetLineFromLineNumber(region.End);

                    //the region starts at the beginning of the "[", and goes until the *end* of the line that contains the "]".
                    //yield return new TagSpan<IOutliningRegionTag>(new SnapshotSpan(startLine.Start, endLine.End), new OutliningRegionTag(false, false, region.Text, "..."));

                //}
            }

        }

        void Reparse() {

            ITextSnapshot newSnapshot = buffer.CurrentSnapshot;
            List<CobolOutliningRegion> newRegions = new List<CobolOutliningRegion>();

            CobolOutliningRegion currentDivision = null;
            CobolOutliningRegion currentSection = null;
            CobolOutliningRegion currentParagraph = null;

            int endOfLastLine = -1;
            int lastLine = -1;
            foreach (var line in newSnapshot.Lines) {
                
                string text = line.GetText();

                if (parsingExpression.IsMatch(text)) {

                    var match = parsingExpression.Match(text);

                    if (text.ToUpper().Contains(" DIVISION")) {

                        if (currentDivision != null) {
                            currentDivision.End = endOfLastLine;
                            currentDivision.EndLine = lastLine;
                            newRegions.Add(currentDivision);
                        }

                        if (currentSection != null) {
                            currentSection.End = endOfLastLine;
                            currentSection.EndLine = lastLine;
                            newRegions.Add(currentSection);
                        }

                        currentDivision = new Outlining.CobolOutliningRegion() {
                            RegionType = CobolOutliningRegionType.Division,
                            Start = line.Start + match.Index,
                            StartLine = line.LineNumber,
                            StartOffset = match.Index,
                            Text = match.Groups["name"].Value,
                            CollapsedText = match.Value
                        };

                        currentSection = null;
                        currentParagraph = null;
                        
                    }

                    if (text.ToUpper().Contains(" SECTION")) {

                        if (currentSection != null) {
                            currentSection.End = endOfLastLine;
                            currentSection.EndLine = lastLine;
                            newRegions.Add(currentSection);
                        }

                        if (currentParagraph != null) {
                            currentParagraph.End = endOfLastLine;
                            currentParagraph.EndLine = lastLine;
                            newRegions.Add(currentParagraph);
                        }

                        currentSection = new Outlining.CobolOutliningRegion() {
                            RegionType = CobolOutliningRegionType.Section,
                            Start = line.Start + match.Index,
                            StartLine = line.LineNumber,
                            StartOffset = match.Index,
                            Text = match.Groups["name"].Value,
                            CollapsedText = match.Value
                        };

                        currentParagraph = null;
                    }

                    if (text.ToUpper().Contains(" DIVISION") == false && text.ToUpper().Contains(" SECTION") == false && currentDivision.Text.ToUpper() == "PROCEDURE") {

                        if (currentParagraph != null) {
                            currentParagraph.End = endOfLastLine;
                            currentParagraph.EndLine = lastLine;
                            newRegions.Add(currentParagraph);
                        }

                        currentParagraph = new Outlining.CobolOutliningRegion() {
                            RegionType = CobolOutliningRegionType.Paragraph,
                            Start = line.Start + match.Index,
                            StartLine = line.LineNumber,
                            StartOffset = match.Index,
                            Text = match.Groups["name"].Value,
                            CollapsedText = match.Value
                        };

                    }

                    

                }

                endOfLastLine = line.End;
                lastLine = line.LineNumber;

            }

            if (currentDivision != null) {
                currentDivision.End = endOfLastLine;
                currentDivision.EndLine = lastLine;
                newRegions.Add(currentDivision);
            }

            if (currentSection != null) {
                currentSection.End = endOfLastLine;
                currentSection.EndLine = lastLine;
                newRegions.Add(currentSection);
            }

            if (currentParagraph != null) {
                currentParagraph.End = endOfLastLine;
                currentParagraph.EndLine = lastLine;
                newRegions.Add(currentParagraph);
            }

            //this.regions = newRegions;




            List<Span> oldSpans = new List<Span>(this.regions.Select(r => AsSnapshotSpan(r, this.snapshot)
                            .TranslateTo(newSnapshot, SpanTrackingMode.EdgeExclusive)
                            .Span));
            List<Span> newSpans = new List<Span>(newRegions.Select(r => AsSnapshotSpan(r, newSnapshot).Span));

            NormalizedSpanCollection oldSpanCollection = new NormalizedSpanCollection(oldSpans);
            NormalizedSpanCollection newSpanCollection = new NormalizedSpanCollection(newSpans);
            NormalizedSpanCollection removed = NormalizedSpanCollection.Difference(oldSpanCollection, newSpanCollection);

            int changeStart = int.MaxValue;
            int changeEnd = -1;

            if (removed.Count > 0) {
                changeStart = removed[0].Start;
                changeEnd = removed[removed.Count - 1].End;
            }

            if (newSpans.Count > 0) {
                changeStart = Math.Min(changeStart, newSpans[0].Start);
                changeEnd = Math.Max(changeEnd, newSpans[newSpans.Count - 1].End);
            }

            this.snapshot = newSnapshot;
            this.regions = newRegions;

            if (changeStart <= changeEnd) {
                ITextSnapshot snap = this.snapshot;
                if (this.TagsChanged != null)
                    this.TagsChanged(this, new SnapshotSpanEventArgs(
                        new SnapshotSpan(this.snapshot, Span.FromBounds(changeStart, changeEnd))));
            }


        }





    }
}
