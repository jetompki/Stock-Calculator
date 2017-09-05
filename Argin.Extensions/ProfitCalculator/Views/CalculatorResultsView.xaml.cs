using Argin.Extensions.ProfitCalculator.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Argin.Extensions.ProfitCalculator.Views
{
    /// <summary>
    /// Interaction logic for CalculatorResults.xaml
    /// </summary>
    public partial class CalculatorResultsView : UserControl
    {
        public CalculatorResultsView()
        {
            InitializeComponent();
            CalculatorResultsViewModel.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        private void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CalculatorResultsViewModel calcResultsVM = sender as CalculatorResultsViewModel;

            if (calcResultsVM == null) return;

            if (e.PropertyName == "DisplayText")
            {
                ColorDisplayText(calcResultsVM);         
            }
        }

        /// <summary>
        /// Colors then rebuilds the string that is displayed in the RichTextBox.
        /// </summary>
        public void ColorDisplayText(CalculatorResultsViewModel calcResultsVM)
        {
            DisplayRichTextBox.Document.Blocks.Clear();
            var profitPercentage = calcResultsVM.ProfitPercentage;
            var profitPercentageString = profitPercentage.ToString() + "%";
            var displayText = calcResultsVM.DisplayText;

            string[] splitter = displayText.Split(new string[] { profitPercentageString }, StringSplitOptions.None);

            if (splitter.Count() == 2)
            {
                AppendText(DisplayRichTextBox, splitter[0], Brushes.Black);

                if (profitPercentage < 0)
                    AppendText(DisplayRichTextBox, profitPercentageString, (FindResource("NegativeLossBrush") as SolidColorBrush));
                else if (profitPercentage > 0)
                    AppendText(DisplayRichTextBox, profitPercentageString, (FindResource("PositiveGainBrush") as SolidColorBrush));
                else
                    AppendText(DisplayRichTextBox, profitPercentageString, Brushes.Black);

                AppendText(DisplayRichTextBox, splitter[1], Brushes.Black);
                DisplayRichTextBox.Document.TextAlignment = TextAlignment.Center;
            }
            else
            {
                AppendText(DisplayRichTextBox, displayText, Brushes.Black);
                DisplayRichTextBox.Document.TextAlignment = TextAlignment.Center;
            }
        }

        /// <summary>
        /// Appends a string to the RichTextBox with the specified color.
        /// </summary>
        public void AppendText(RichTextBox box, string text, string color)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty,
                    bc.ConvertFromString(color));
            }
            catch (FormatException) { }
        }

        /// <summary>
        /// Appends a string to the RichTextBox with the specified color.
        /// </summary>
        public void AppendText(RichTextBox box, string text, Brush brush)
        {
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, brush);
            }
            catch (FormatException) { }
        }
    }
}
