using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Cobol4VisualStudio.Extension.Margins {
    
    public class CobolListBoxItem : ListViewItem {
        public int Start { get; set; }
        public int LineNumber { get; set; }

        public CobolListBoxItem(string text) {
            this.Content = text;
        }
    }
    
    public enum CobolTreeViewItemType {
        Division,
        Section,
        Paragraph
    }

    public class CobolTreeViewItem : TreeViewItem {
        //public string Text { get; set; }
        public int Start { get; set; }
        public CobolTreeViewItemType Type { get; set; }
        //public List<CobolTreeViewItem> Items { get; set; }

        public CobolTreeViewItem(string text) {
            this.Header = text;
            //Items = new List<Margins.CobolTreeViewItem>();
        }
    }

    public class CobolDocumentView {
        private readonly ITextView textView;
        private readonly DispatcherTimer updaterDocument;
        private readonly DispatcherTimer updaterPosition;
        private readonly DispatcherTimer updaterCaret;

        private const double RefreshAfterEditInSeconds = 0.3;
        private const double RefreshAfterNewPositionInSeconds = 0.1;


        public CobolDocumentView(ITextView textView) {
            this.textView = textView;

            updaterDocument = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(RefreshAfterEditInSeconds) };
            updaterDocument.Tick += UpdaterDocumentOnTick;
            textView.TextBuffer.Changed += OnDocumentChanged;

            updaterPosition = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(RefreshAfterNewPositionInSeconds) };
            updaterPosition.Tick += UpdaterPositionOnTick;
            textView.LayoutChanged += OnPositionChanged;

            updaterCaret = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(RefreshAfterEditInSeconds) };
            updaterCaret.Tick += UpdaterCaretOnTick;
            textView.Caret.PositionChanged += OnCaretPositionChanged;

            textView.Closed += TextViewOnClosed;

        }

        public event EventHandler DocumentChanged;

        public event EventHandler PositionChanged;

        public event EventHandler CaretChanged;


        public static CobolDocumentView Get(ITextView textView) {
            return textView.Properties.GetOrCreateSingletonProperty(() => new CobolDocumentView(textView));
        }

        private void UpdaterDocumentOnTick(object sender, EventArgs eventArgs) {
            updaterDocument.Stop();
            DocumentChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdaterPositionOnTick(object sender, EventArgs eventArgs) {
            updaterPosition.Stop();
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdaterCaretOnTick(object sender, EventArgs eventArgs) {
            updaterCaret.Stop();
            CaretChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnDocumentChanged(object sender, TextContentChangedEventArgs e) {
            updaterDocument.Stop();
            updaterDocument.Start();
        }

        private void OnPositionChanged(object sender, TextViewLayoutChangedEventArgs textViewLayoutChangedEventArgs) {
            // Notifies a position update (but don't cancel the previous update if a new update is coming in between)
            //_updaterPosition.Stop();
            updaterPosition.Start();
        }

        private void OnCaretPositionChanged(object sender, CaretPositionChangedEventArgs e) {
            // For the caret, we will wait for a pause before trying to update
            updaterCaret.Stop();
            updaterCaret.Start();
        }

        private void TextViewOnClosed(object sender, EventArgs eventArgs) {
            // Make sure timers are stopped
            updaterDocument.Stop();
            updaterPosition.Stop();
            updaterCaret.Stop();

            textView.LayoutChanged -= OnPositionChanged;

            var textBuffer = textView.TextBuffer;
            if (textBuffer != null)
                textBuffer.Changed -= OnDocumentChanged;
        }

    }

    [Export(typeof(IWpfTextViewMarginProvider))]
    [Name("CobolMarginRightFactory")]
    [Order(After = PredefinedMarginNames.RightControl)]
    [MarginContainer(PredefinedMarginNames.Right)]
    [ContentType("Cobol")]
    [TextViewRole(PredefinedTextViewRoles.Debuggable)]
    public class TestMarginProvider : IWpfTextViewMarginProvider {

        [Import]
        public ITextDocumentFactoryService TextDocumentFactoryService { get; set; }

        public IWpfTextViewMargin CreateMargin(IWpfTextViewHost wpfTextViewHost, IWpfTextViewMargin marginContainer) {
            ITextDocument document;

            if (!TextDocumentFactoryService.TryGetTextDocument(wpfTextViewHost.TextView.TextDataModel.DocumentBuffer, out document)) {
                return null;
            }

            return wpfTextViewHost.TextView.Properties.GetOrCreateSingletonProperty(() => new TestMargin(wpfTextViewHost.TextView, document));

        }
    }

    public class TestMargin : DockPanel, IWpfTextViewMargin {

        private static Regex parsingExpression = new Regex(@"\b((?<name>IDENTIFICATION|ID|ENVIRONMENT|DATA|PROCEDURE)[\s]+DIVISION(?=\.|[\s]??(USING|GIVING|RETURNING))|(?<name>[A-Za-z0-9-]+)[\s]+SECTION\.|(?<=(^[\d]{6}\s{1,4}))(?<![-])(?=[A-Za-z0-9-]*?[A-Z])(?<![-])\b(?<name>[A-Za-z0-9-]{1,30})\b(?!-)(?=\.))", RegexOptions.IgnoreCase);
        private readonly ITextDocument _document;
        private readonly ITextView _textView;

        public TreeView Tree { get; private set; }
        public ListBox ListBox { get; private set; }

        public TestMargin(ITextView textView, ITextDocument document) {

            _textView = textView;
            _document = document;

            TreeView tv = new TreeView();
            this.Tree = tv;

            ListBox lb = new ListBox();

            //Style style = new Style();
            //style.TargetType = typeof(ListBoxItem);
            //style.Setters.Add(new Setter(ListBoxItem.BackgroundProperty, Brushes.Aqua));
            
            //DataTrigger trigger = new DataTrigger();
            //trigger.Binding = new Binding() { Path = new PropertyPath(ListBoxItem.IsSelectedProperty), RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ListBoxItem), 1) };
            //trigger.Value = true;
            //trigger.Setters.Add(new Setter(ListBoxItem.BackgroundProperty, Brushes.Red));
            //style.Triggers.Add(trigger);
            //lb.ItemContainerStyle = style;
            
            //TreeViewItem tvi = new TreeViewItem() {
            //    Header = "Program",
            //    ItemsSource = new string[] { "IDENTIFICATION", "ENVIRONMENT", "DATA", "PROCEDURE" }
            //};

            //Tree.Items.Add(tvi);

            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(this.MarginSize, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition());
            tv.MouseDoubleClick += Tv_MouseDoubleClick;
            lb.MouseDoubleClick += Lb_MouseDoubleClick;
            //grid.Children.Add(tv);
            grid.Children.Add(lb);
            Children.Add(grid);

            Grid.SetColumn(lb, 2);
            Grid.SetRow(lb, 0);
           // Grid.SetColumn(tv, 2);
            //Grid.SetRow(tv, 0);

            GridSplitter splitter = new GridSplitter();
            splitter.Width = 5;
            splitter.ResizeDirection = GridResizeDirection.Columns;
            splitter.VerticalAlignment = VerticalAlignment.Stretch;
            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
            splitter.DragCompleted += RightDragCompleted;

            grid.Children.Add(splitter);
            Grid.SetColumn(splitter, 1);
            Grid.SetRow(splitter, 0);


            //textView.TextBuffer.Changed += TextBuffer_Changed;
            //textView.LayoutChanged += TextView_LayoutChanged;
            //textView.Caret.PositionChanged += Caret_PositionChanged;
            var view = CobolDocumentView.Get(textView);
            view.DocumentChanged += View_DocumentChanged;
            view.PositionChanged += View_PositionChanged;
            view.CaretChanged += View_CaretChanged;
            View_DocumentChanged(this, EventArgs.Empty);
            
            this.Tree = tv;
            this.ListBox = lb;
            this.ListBox.DisplayMemberPath = "Text";
        }

        private void Lb_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            CobolListBoxItem item = ListBox.SelectedItem as CobolListBoxItem;
            _textView.DisplayTextLineContainingBufferPosition(new SnapshotPoint(_textView.TextBuffer.CurrentSnapshot, item.Start), 10, ViewRelativePosition.Top);
        }

        private async void View_CaretChanged(object sender, EventArgs e) {
            var linenumber = _textView.TextSnapshot.GetLineNumberFromPosition(_textView.Caret.Position.BufferPosition);
            await Dispatcher.BeginInvoke(new Action(() => {
                System.Diagnostics.Debug.WriteLine("Caret Position: {0}", linenumber);
                
                for (int i = 0; i < ListBox.Items.Count; i++) {
                    CobolListBoxItem item = ListBox.Items[i] as CobolListBoxItem;
                    if (item.LineNumber == linenumber) {
                        ListBox.SelectedIndex = i;
                    }
                    
                    if (item.LineNumber > linenumber && i > 0 && (ListBox.Items[i - 1] as CobolListBoxItem).LineNumber < linenumber) {
                        ListBox.SelectedIndex = i - 1;
                    }
                }

            }), DispatcherPriority.ApplicationIdle, null);
        }

        private void Tv_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            CobolTreeViewItem item = Tree.SelectedItem as CobolTreeViewItem;
            _textView.DisplayTextLineContainingBufferPosition(new SnapshotPoint(_textView.TextBuffer.CurrentSnapshot, item.Start), 10, ViewRelativePosition.Top);
        }

        private async void View_PositionChanged(object sender, EventArgs e) {
            var linenumber = _textView.TextSnapshot.GetLineNumberFromPosition(_textView.TextViewLines.FirstVisibleLine.Start.Position);
            await Dispatcher.BeginInvoke(new Action(() => {
                System.Diagnostics.Debug.WriteLine("Cursor Position: {0}", linenumber);
            }), DispatcherPriority.ApplicationIdle, null);
        }

        private async void View_DocumentChanged(object sender, EventArgs e) {
            //System.Diagnostics.Debug.WriteLine("Document Changed");
            await UpdateTree(_document.TextBuffer.CurrentSnapshot);
        }

        private async Task UpdateTree(ITextSnapshot snapshot) {
            //System.Diagnostics.Debug.WriteLine("Update Tree");

            await this.Dispatcher.BeginInvoke(new Action(() => {
                System.Diagnostics.Debug.WriteLine("Update Tree Asnyc");
                Tree.Items.Clear();
                ListBox.Items.Clear();

            

            CobolTreeViewItem currentDivision = null;
            CobolTreeViewItem currentSection = null;
            CobolTreeViewItem currentParagraph = null;

            foreach (var line in snapshot.Lines) {
                int linenumber = line.LineNumber;
                string text = line.GetText();

                    
                if (parsingExpression.IsMatch(text)) {
                    Match m = parsingExpression.Match(text);

                        

                        if (m.Value.ToUpper().Contains(" DIVISION")) {
                            currentDivision = new CobolTreeViewItem(m.Value) { Start = line.Start + m.Index, Type = CobolTreeViewItemType.Division, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Color.FromRgb(0x66, 0x00, 0x66)), IsExpanded = true, Padding = new Thickness(0, 5, 0, 2) };
                            currentSection = null;
                            currentParagraph = null;
                            Tree.Items.Add(currentDivision);

                            //ListBoxItem lbi = new ListBoxItem() { Content = new { Text = m.Value, Start = line.Start + m.Index, LineNumber = linenumber }, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Color.FromRgb(0x66, 0x00, 0x66)) };
                            CobolListBoxItem lbi = new CobolListBoxItem(m.Value) { FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Color.FromRgb(0x66, 0x00, 0x66)), LineNumber = linenumber, Start = line.Start + m.Index, Padding = new Thickness(0, 5, 0, 2) };
                            ListBox.Items.Add(lbi);

                            //Tree.Items.Add(new CobolTreeViewItem(m.Value) { Type = CobolTreeViewItemType.Division, FontWeight = FontWeights.Bold,  });

                            //Tree.Items.Add(new CobolTreeViewItem() { Text = m.Value, Type = CobolTreeViewItemType.Division });
                            //items.Add(
                            //Tree.Items.Add(new CobolTreeViewItem() { Text = m.Value, Type = CobolTreeViewItemType.Division });
                            //items.Add(new CobolTreeViewItem() { Text = m.Value, Type = CobolTreeViewItemType.Division });
                        }
                        else if (m.Value.ToUpper().Contains(" SECTION")) {
                            currentSection = new CobolTreeViewItem(m.Value) { Start = line.Start + m.Index, Type = CobolTreeViewItemType.Section, FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Color.FromRgb(0x99, 0x00, 0x99)), IsExpanded = true };
                            currentParagraph = null;
                            currentDivision.Items.Add(currentSection);

                            CobolListBoxItem lbi = new CobolListBoxItem("    " + m.Value) { FontWeight = FontWeights.Bold, Foreground = new SolidColorBrush(Color.FromRgb(0x99, 0x00, 0x99)), LineNumber = linenumber, Start = line.Start + m.Index };
                            ListBox.Items.Add(lbi);
                            //items.Last().Items.Add(new CobolTreeViewItem() { Text = m.Value, Type = CobolTreeViewItemType.Section });
                            //(Tree.Items[Tree.Items.Count - 1] as TreeViewItem).Items.Add(new CobolTreeViewItem() { Text = m.Value, Type = CobolTreeViewItemType.Section });
                        }
                        else {
                            currentParagraph = new CobolTreeViewItem(m.Value) { Start = line.Start + m.Index, Type = CobolTreeViewItemType.Paragraph, FontWeight = FontWeights.SemiBold, Foreground = new SolidColorBrush(Color.FromRgb(0xCC, 0x00, 0xCC)), IsExpanded = true };
                            if (currentSection == null) {
                                currentDivision.Items.Add(currentParagraph);
                            }
                            else {
                                currentSection.Items.Add(currentParagraph);
                            }

                            CobolListBoxItem lbi = new CobolListBoxItem("        " + m.Value) { FontWeight = FontWeights.SemiBold, Foreground = new SolidColorBrush(Color.FromRgb(0xCC, 0x00, 0xCC)), LineNumber = linenumber, Start = line.Start + m.Index };
                            ListBox.Items.Add(lbi);

                        }

                        //TreeViewItem i = new TreeViewItem();
                        //i.Header = m.Value;
                        //Tree.Items.Add(i);
                        
                    }
                }

                //HierarchicalDataTemplate hdt = new HierarchicalDataTemplate();
                //hdt.ItemsSource = new Binding("Items");
                //FrameworkElementFactory tb = new FrameworkElementFactory(typeof(TextBlock));
                //tb.SetBinding(TextBlock.TextProperty, new Binding("Text"));
                //hdt.VisualTree = tb;
                //Tree.ItemTemplate = hdt;
                //Tree.ItemsSource = items;
                //Tree.ItemTemplate = new HierarchicalDataTemplate(typeof(CobolTreeViewItem));


            }), DispatcherPriority.ApplicationIdle, null);
        }

        private void RightDragCompleted(object sender, DragCompletedEventArgs e) {

            if (!double.IsNaN(Tree.ActualWidth)) {
                
            }

        }

        public bool Enabled {
            get {
                return true;
            }
        }

        public double MarginSize {
            get {
                return 300;
            }
        }

        public FrameworkElement VisualElement {
            get {
                return this;
            }
        }

        public void Dispose() {
            
        }

        public ITextViewMargin GetTextViewMargin(string marginName) {
            return this;
        }
    }
}
