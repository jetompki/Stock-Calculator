using System;

namespace Argin.Extensions.ProfitCalculator.Events
{
    public class InputChangedEventArgs : EventArgs
    {
        private readonly double? _initialSharePrice;
        public double? InitialSharePrice { get { return _initialSharePrice; } }

        private readonly double? _finalSharePrice;
        public double? FinalSharePrice { get { return _finalSharePrice; } }

        private readonly double? _profitPercentage;
        public double? ProfitPercentage { get { return _profitPercentage; } }

        private readonly double? _takerFee;
        public double? TakerFee { get { return _takerFee; } }

        private readonly double? _makerFee;
        public double? MakerFee { get { return _makerFee; } }

        private readonly double? _allotment;
        public double? Allotment { get { return _allotment; } }

        public InputChangedEventArgs(double? initial, double? final, double? profitPercent, double? maker, double? taker, double? allotment)
        {
            _initialSharePrice = initial;
            _finalSharePrice = final;
            _profitPercentage = profitPercent;
            _makerFee = maker;
            _takerFee = taker;
            _allotment = allotment;
        }
    }
}
