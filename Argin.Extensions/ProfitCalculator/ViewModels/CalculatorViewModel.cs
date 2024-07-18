using Argin.Extensions.ProfitCalculator.Data;
using Argin.Extensions.ProfitCalculator.Events;


namespace Argin.Extensions.ProfitCalculator.ViewModels
{
    /// <summary>
    /// The calculator with references to its components.
    /// </summary>
    public class CalculatorViewModel : ViewModelBase
    {
        private static readonly CalculatorViewModel _instance = new CalculatorViewModel();
        public static CalculatorViewModel Instance { get { return _instance; } }

        public CalculatorInputViewModel CalcInputVM { get { return CalculatorInputViewModel.Instance; } }
        public CalculatorResultsViewModel CalcResultsVM { get { return CalculatorResultsViewModel.Instance; } }

        private Processor _processor;
        public Processor Processor
        {
            get { return _processor; }
            set { _processor = value; }
        }

        public CalculatorViewModel()
        {
            _processor = new Processor(CalcResultsVM);
            CalcInputVM.InputChanged += CalcInputVM_InputChanged;
        }

        private void CalcInputVM_InputChanged(object sender, InputChangedEventArgs e)
        {
            Processor.ProcessInput(e);
        }
    }
}
