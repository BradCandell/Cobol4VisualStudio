using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Adornments {

    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("Cobol")]
    [TextViewRole(PredefinedTextViewRoles.Interactive)]
    internal sealed class ColumnGuideAdornmentProvider : IWpfTextViewCreationListener {


        private bool isFirstLayoutDone = false;
        private static double guideThickness = 1.0;
        private static Brush guideBrush = new SolidColorBrush(Colors.LightGray);
        private static DoubleCollection guideDashes = new DoubleCollection(new double[] { 1.0, 3.0 });
        private List<Line> guidelines = new List<Line>();
        private IWpfTextView view;
        private double baseIndentation;
        private double columnWidth;



        public void TextViewCreated(IWpfTextView textView) {
            view = textView;
            view.LayoutChanged += View_LayoutChanged;
            view.Closed += View_Closed;

            guidelines = CreateGuidelines();

        }

        private void View_Closed(object sender, EventArgs e) {
            view.LayoutChanged -= View_LayoutChanged;
            view.Closed -= View_Closed;
        }

        private void View_LayoutChanged(object sender, TextViewLayoutChangedEventArgs e) {

            bool shouldUpdatePositions = false;

            IFormattedLineSource lineSource = view.FormattedLineSource;
            if (lineSource == null) {
                return;
            }

            if (columnWidth != lineSource.ColumnWidth) {
                columnWidth = lineSource.ColumnWidth;
                shouldUpdatePositions = true;
            }
            if (baseIndentation != lineSource.BaseIndentation) {
                baseIndentation = lineSource.BaseIndentation;
                shouldUpdatePositions = true;
            }

            if (shouldUpdatePositions ||
                e.VerticalTranslation ||
                e.NewViewState.ViewportTop != e.OldViewState.ViewportTop ||
                e.NewViewState.ViewportBottom != e.OldViewState.ViewportBottom) {
                UpdatePositions();
            }

            if (!isFirstLayoutDone) {
                AddGuidelinesToAdornmentLayer();
                isFirstLayoutDone = true;
            }

        }


        private void UpdatePositions() {

            foreach (Line line in guidelines) {
                int column = (int)line.DataContext;
                line.X2 = baseIndentation + 0.5 + column * columnWidth;
                line.X1 = line.X2;
                line.Y1 = view.ViewportTop;
                line.Y2 = view.ViewportBottom;
            }

        }

        private void AddGuidelinesToAdornmentLayer() {

            IAdornmentLayer adornmentLayer = view.GetAdornmentLayer("CobolColumnGuideAdornment");

            if (adornmentLayer == null) {
                return;
            }

            adornmentLayer.RemoveAllAdornments();

            foreach (UIElement element in guidelines) {
                adornmentLayer.AddAdornment(AdornmentPositioningBehavior.OwnerControlled, null, null, element, null);
            }

        }

        private static List<Line> CreateGuidelines() {

            List<Line> results = new List<Line>();
            results.Add(CreateDefaultGuideline(6));
            results.Add(CreateDefaultGuideline(7));
            results.Add(CreateDefaultGuideline(72));

            return results;

        }

        private static Line CreateDefaultGuideline(int column) {

            Line guide = new Line() {
                DataContext = column,
                Stroke = guideBrush,
                StrokeThickness = guideThickness,
                StrokeDashArray = guideDashes
            };

            return guide;

        } 


    }

}
