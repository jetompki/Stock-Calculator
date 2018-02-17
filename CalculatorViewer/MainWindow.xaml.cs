using Argin.Extensions.ProfitCalculator.Views;
using System.Windows;

namespace CalculatorViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Visibility = Visibility.Collapsed;

            CalculatorView calculatorView = new CalculatorView();
            calculatorView.Closed += CalculatorView_Closed;
            calculatorView.ShowDialog();
        }

        private void CalculatorView_Closed(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
