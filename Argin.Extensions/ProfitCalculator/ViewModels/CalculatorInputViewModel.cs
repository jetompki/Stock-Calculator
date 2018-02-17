using Argin.Extensions.ProfitCalculator.Events;
using System;

namespace Argin.Extensions.ProfitCalculator.ViewModels
{
    /// <summary>
    /// Holds the information related to user input for the calculator. [Design Pattern: Singleton]
    /// </summary>
    public class CalculatorInputViewModel : ViewModelBase
    {
        public static readonly CalculatorInputViewModel _instance = new CalculatorInputViewModel();
        public static CalculatorInputViewModel Instance { get { return _instance; } }

        private CalculatorInputViewModel() { }

        private double? _initialSharePrice;
        public double? InitialSharePrice
        {
            get { return _initialSharePrice; }
            set
            {
                _initialSharePrice = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private double? _finalSharePrice;
        public double? FinalSharePrice
        {
            get { return _finalSharePrice; }
            set
            {
                _finalSharePrice = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private double? _profitPercentage;
        public double? ProfitPercentage
        {
            get { return _profitPercentage; }
            set
            {
                _profitPercentage = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private double? _makerFee;
        public double? MakerFee
        {
            get { return _makerFee; }
            set
            {
                _makerFee = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private double? _takerFee;
        public double? TakerFee
        {
            get { return _takerFee; }
            set
            {
                _takerFee = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private double? _allotment;
        public double? Allotment
        {
            get { return _allotment; }
            set
            {
                _allotment = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private bool _takerFeePercentageChecked = false;
        public bool TakerFeePercentageChecked
        {
            get { return _takerFeePercentageChecked; }
            set
            {
                _takerFeePercentageChecked = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        private bool _makerFeePercentageChecked = false;
        public bool MakerFeePercentageChecked
        {
            get { return _makerFeePercentageChecked; }
            set
            {
                _makerFeePercentageChecked = value;
                if (IsCalculatorInputComplete) OnInputChanged();
            }
        }

        public bool IsCalculatorInputComplete
        {
            get
            {
                bool method = _initialSharePrice != null && _finalSharePrice != null && _makerFee != null && _takerFee != null && _allotment != null;

                if (!method)
                    method = _initialSharePrice != null && _makerFee != null && _takerFee != null && _allotment != null && _profitPercentage != null;

                return method;
            }
        }

        public event EventHandler<InputChangedEventArgs> InputChanged;

        protected virtual void OnInputChanged()
        {
            InputChanged?.Invoke(this, new InputChangedEventArgs(_initialSharePrice, _finalSharePrice, _profitPercentage, _makerFee, _takerFee, _allotment, _makerFeePercentageChecked, _takerFeePercentageChecked));
        }
    }
}
