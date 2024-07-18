namespace Argin.Extensions.ProfitCalculator.ViewModels
{
    /// <summary>
    /// Holds the results of the calculator after processing the input. [Design Pattern: Singleton]
    /// </summary>
    public class CalculatorResultsViewModel : ViewModelBase
    {
        public static readonly CalculatorResultsViewModel _instance = new CalculatorResultsViewModel();
        public static CalculatorResultsViewModel Instance { get { return _instance; } }

        private CalculatorResultsViewModel() { }

        private bool _textToggle;
        public bool IsTextToggled
        {
            get { return _textToggle; }
            set
            {
                if (_textToggle != value)
                {
                    _textToggle = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _displayText;
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _profitPercentage;
        public double? ProfitPercentage
        {
            get { return _profitPercentage; }
            set
            {
                if (_profitPercentage != value)
                {
                    _profitPercentage = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _proceeds;
        public double? Proceeds
        {
            get { return _proceeds; }
            set
            {
                if (_proceeds != value)
                {
                    _proceeds = value;
                    OnPropertyChanged();
                }
            }
        }


        private double? _cost;
        public double? Cost
        {
            get { return _cost; }
            set
            {
                if (_cost != value)
                {
                    _cost = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _netProfit;
        public double? NetProfit
        {
            get { return _netProfit; }
            set
            {
                if (_netProfit != value)
                {
                    _netProfit = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _roi;
        public double? RoI
        {
            get { return _roi; }
            set
            {
                if (_roi != value)
                {
                    _roi = value;
                    OnPropertyChanged();
                }
            }
        }

        private double? _finalSharePrice;
        public double? FinalSharePrice
        {
            get { return _finalSharePrice; }
            set
            {
                if (_finalSharePrice != value)
                {
                    _finalSharePrice = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
