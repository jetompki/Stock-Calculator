using Argin.Extensions.ProfitCalculator.Events;
using Argin.Extensions.ProfitCalculator.ViewModels;
using Argin.Extensions.Resources;
using System;

namespace Argin.Extensions.ProfitCalculator.Data
{
    /// <summary>
    /// Receives the input from the CalculatorInputViewModel, calculates the results, then transfers to CalculatorResultsViewModel.
    /// </summary>
    public class Processor
    {
        private enum State
        {
            Idle,
            Processing
        }

        private State _state;

        private CalculatorResultsViewModel _calcResultsVM;

        public Processor(CalculatorResultsViewModel calcResultsVM)
        {
            _calcResultsVM = calcResultsVM;
        }

        public void ProcessInput(InputChangedEventArgs e)
        {
            if (_state == State.Idle)
            {
                _state = State.Processing;

                _calcResultsVM.ProfitPercentage = e.ProfitPercentage;
                _calcResultsVM.IsTextToggled = e.ProfitPercentage == null || e.ProfitPercentage == 0 ? false : true;

                if (_calcResultsVM.IsTextToggled)
                    _calcResultsVM.DisplayText = string.Format(CalculatorStrings.FinalShareReturnText, e.ProfitPercentage);
                else
                    _calcResultsVM.DisplayText = CalculatorStrings.BreakEvenText;

                double? proceeds = CalculateProceeds(e.FinalSharePrice, e.Allotment);
                double? cost = CalculateCost(e.InitialSharePrice, e.FinalSharePrice, e.TakerFee, e.MakerFee, e.Allotment, e.TakerFeePercentageChecked, e.MakerFeePercentageChecked);
                double? netProfit = CalculateNetProfit(proceeds, cost, e.InitialSharePrice, e.Allotment);
                double? roi = CalculateRoI(netProfit, cost, e.InitialSharePrice, e.Allotment);
                double? finalSharePrice = CalculateDisplayedFinalSharePrice(e.InitialSharePrice, e.FinalSharePrice, e.ProfitPercentage, e.TakerFee, e.MakerFee, e.Allotment, e.TakerFeePercentageChecked, e.MakerFeePercentageChecked);

                _calcResultsVM.Proceeds = proceeds;
                _calcResultsVM.Cost = cost;
                _calcResultsVM.NetProfit = netProfit;

                if (roi != null)
                    _calcResultsVM.RoI = Math.Round((double)roi, 2);
                else
                    _calcResultsVM.RoI = roi;

                if (finalSharePrice != null)
                    _calcResultsVM.FinalSharePrice = finalSharePrice;

                _state = State.Idle;
            }
        }

        private double? CalculateRoI(double? netProfit, double? cost, double? initialSharePrice, double? allotment)
        {
            return (netProfit / cost) * 100;
        }

        private double? CalculateNetProfit(double? proceeds, double? cost, double? initial, double? allotment)
        {
            return proceeds - cost;
        }

        /// <summary>
        /// Given the Inital Share Price, Final Share Price Maker Fee, Taker Fee, and Allotment, calculate the total cost after buying then selling. 
        /// Uses an alternate method depending on type of Maker/Taker Fee (Percent or Flat).
        /// </summary>
        /// <returns>The total cost of purchasing and selling.</returns>
        private double? CalculateCost(double? initialSharePrice, double? finalSharePrice, double? takerFee, double? makerFee, double? allotment, bool useTakerFeePercent, bool useMakerFeePercent)
        {
            var part1 = useTakerFeePercent ? (initialSharePrice * allotment) + (initialSharePrice * allotment) * (takerFee / 100) : initialSharePrice * allotment + takerFee;
            var part2 = useMakerFeePercent ? part1 + (finalSharePrice * allotment) * (makerFee / 100) : part1 + makerFee;
            return part2;
        }

        private double? CalculateProceeds(double? finalSharePrice, double? allotment)
        {
            return finalSharePrice * allotment;
        }

        /// <summary>
        /// Given Initial Share Price, Maker Fee, Taker Fee calculate FinalSharePrice such that NetProfit == 0.
        /// Uses an alternate method depending on type of Maker/Taker Fee (Percent or Flat).
        /// </summary>
        /// <returns>FinalSharePrice that will result in neither profit nor loss.</returns>
        private double? CalculateBreakEvenPrice(double? initialSharePrice, double? allotment, double? takerFee, double? makerFee, bool useTakerFeePercent, bool useMakerFeePercent)
        {
            var part1 = useTakerFeePercent ? (initialSharePrice * allotment) + (initialSharePrice * allotment) * (takerFee / 100) : (initialSharePrice * allotment) + takerFee;
            var part2 = useMakerFeePercent ? part1 / (allotment * (1 - makerFee / 100)) : (part1 + makerFee) / allotment;
            return part2;
        }

        /// <summary>
        /// Given Initial Share Price, Allotment, Maker Fee, Taker Fee, profitPercentage  calculate FinalSharePrice such that RoI == profitPercentage.
        /// Uses an alternate method depending on type of Maker/Taker Fee (Percent or Flat).
        /// </summary>
        /// <returns>FinalSharePrice that will obtain the desired profit.</returns>
        private double? CalculatedDesiredReturnPrice(double? initialSharePrice, double? allotment, double? takerFee, double? makerFee, double? profitPercentage, bool useTakerFeePercent, bool useMakerFeePercent)
        {
            // This will result in an invalid denominator. 
            if (makerFee * (profitPercentage + 100) == 10000 || allotment == 0 || (allotment * initialSharePrice + takerFee) == 0)
            {
                return double.NaN;
            }

            var part1 = useTakerFeePercent ? -1 * (initialSharePrice * (profitPercentage + 100) * (takerFee + 100)) : (profitPercentage + 100) * (allotment * initialSharePrice + takerFee);
            var part2 = useMakerFeePercent ? part1 / (makerFee * (profitPercentage + 100) - 10000) : (part1 + 100 * makerFee) / (allotment * 100);

            var result = part2;

            // The input resulted in an invalid calculation. Most likely the maker fee was too high relative to the desired profitPercentage
            if (result < 0)
            {
                return double.NaN;
            }

            return result;
        }

        /// <summary>
        /// Calculates the DisplayedFinalSharePrice which is either the BreakEvenPrice or the DesiredReturnPrice, depending on how the user has filled out the input view.
        /// </summary>
        /// <returns></returns>
        private double? CalculateDisplayedFinalSharePrice(double? initialSharePrice, double? finalSharePrice, double? profitPercentage, double? takerFee, double? makerFee, double? allotment, bool useTakerFeePercent, bool useMakerFeePercent)
        {
            if (makerFee >= 100)
            {
                // The cost to sell exceeds the proceeds. Cannot reach break even price.
                return double.NaN;
            }

            if (profitPercentage == null || profitPercentage == 0.0)
            {
                return CalculateBreakEvenPrice(initialSharePrice, allotment, takerFee, makerFee, useTakerFeePercent, useMakerFeePercent);
            }

            return CalculatedDesiredReturnPrice(initialSharePrice, allotment, takerFee, makerFee, profitPercentage, useTakerFeePercent, useMakerFeePercent);
        }
    }
}
