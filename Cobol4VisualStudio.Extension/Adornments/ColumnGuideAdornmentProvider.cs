using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Adornments {

    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("CobolNone")]
    [TextViewRole(PredefinedTextViewRoles.Interactive)]
    internal sealed class ColumnGuideAdornmentProvider : IWpfTextViewCreationListener {


        private bool isFirstLayoutDone = false;
        private const double guideThickness = 1.0;
        private List<Line> guidelines = new List<Line>();

        public IWpfTextView View { get; private set; }
        
        public void TextViewCreated(IWpfTextView textView) {
            View = textView;
            View.LayoutChanged += View_LayoutChanged;
            View.Closed += View_Closed;
        }

        private void View_Closed(object sender, EventArgs e) {
            View.LayoutChanged -= View_LayoutChanged;
            View.Closed -= View_Closed;
        }

        private void View_LayoutChanged(object sender, TextViewLayoutChangedEventArgs e) {

            IAdornmentLayer adornmentLayer = View.GetAdornmentLayer("ColumnGuideAdornment");

            if (adornmentLayer == null) {
                return;
            }

            DoubleCollection dashArray = new DoubleCollection(new double[] { 1.2, 1.2 });
            adornmentLayer.RemoveAllAdornments();

            double columnWidth = 1;
            double baseIndentation = 0;

            var ls = View.FormattedLineSource;
            if (ls != null) {
                columnWidth = View.FormattedLineSource.ColumnWidth;
                baseIndentation = View.FormattedLineSource.BaseIndentation;
            }

            Brush b = new SolidColorBrush(Colors.Red);
            Line line1 = new Line() {
                // Use the DataContext slot as a cookie to hold the column
                DataContext = 1,
                Stroke = b,
                StrokeThickness = 1.0,
                StrokeDashArray = dashArray,
                Opacity = 0.5,
                //Y1 = _view.ViewportTop,
                //Y2 = _view.ViewportBottom,
                //X1 = baseIndentation + (columnWidth * 6) + 0.5,
                //X2 = baseIndentation + (columnWidth * 6) + 0.5
            };

        }


    }

}
