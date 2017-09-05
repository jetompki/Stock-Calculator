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
                double? cost = CalculateCost(e.InitialSharePrice, e.FinalSharePrice, e.TakerFee, e.MakerFee, e.Allotment);
                double? netProfit = CalculateNetProfit(proceeds, cost, e.InitialSharePrice, e.Allotment);
                double? roi = CalculateRoI(netProfit, cost, e.InitialSharePrice, e.Allotment);
                double? finalSharePrice = CalculateDisplayedFinalSharePrice(e.InitialSharePrice, e.FinalSharePrice, e.ProfitPercentage, e.TakerFee, e.MakerFee, e.Allotment);

                _calcResultsVM.Proceeds = proceeds;
                _calcResultsVM.Cost = cost;
                _calcResultsVM.NetProfit = netProfit;

                if (roi != null)
                    _calcResultsVM.RoI = Math.Round((double)roi, 2);
                else
                    _calcResultsVM.RoI = roi;

                if (finalSharePrice != null)
                    _calcResultsVM.FinalSharePrice = Math.Round((double)finalSharePrice, 2);
                else
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
        /// </summary>
        /// <returns>The total cost of purchasing and selling.</returns>
        private double? CalculateCost(double? initialSharePrice, double? finalSharePrice, double? takerFee, double? makerFee, double? allotment)
        {
            return (initialSharePrice * allotment) + (initialSharePrice * allotment) * (takerFee / 100) + (finalSharePrice * allotment) * (makerFee / 100);
        }

        private double? CalculateProceeds(double? finalSharePrice, double? allotment)
        {
            return finalSharePrice * allotment;
        }

        /// <summary>
        /// Given Initial Share Price, Maker Fee, Taker Fee  calculate FinalSharePrice such that NetProfit == 0.
        /// </summary>
        /// <returns>FinalSharePrice that will result in neither profit nor loss.</returns>
        private double? CalculateBreakEvenPrice(double? initialSharePrice, double? allotment, double? takerFee, double? makerFee)
        {
            return ((initialSharePrice * allotment) + (initialSharePrice * allotment * takerFee / 100)) / (allotment * (1 - makerFee / 100));
        }

        /// <summary>
        /// Given Initial Share Price, Allotment, Maker Fee, Taker Fee, profitPercentage  calculate FinalSharePrice such that RoI == profitPercentage.
        /// </summary>
        /// <returns>FinalSharePrice that will obtain the desired profit.</returns>
        private double? CalculatedDesiredReturnPrice(double? initialSharePrice, double? allotment, double? takerFee, double? makerFee, double? profitPercentage)
        {
            if (100 - makerFee < profitPercentage)
            {
                // The desired percentage exceeds the percentage of proceeds being received.
                return double.NaN;
            }

            if (makerFee * (profitPercentage + 100) == 10000 || allotment == 0)
            {
                // This will result in an invalid denominator. 
                return double.NaN;
            }

            return -1 * (initialSharePrice * (profitPercentage + 100) * (takerFee + 100)) / (makerFee * (profitPercentage + 100) - 10000);
        }

        private double? CalculateDisplayedFinalSharePrice(double? initialSharePrice, double? finalSharePrice, double? profitPercentage, double? takerFee, double? makerFee, double? allotment)
        {
            if (makerFee >= 100)
            {
                // The cost to sell exceeds the proceeds. Cannot reach break even price.
                return double.NaN;
            }

            if (profitPercentage == null || profitPercentage == 0.0)
            {              
                return CalculateBreakEvenPrice(initialSharePrice, allotment, takerFee, makerFee);
            }

             return CalculatedDesiredReturnPrice(initialSharePrice, allotment, takerFee, makerFee, profitPercentage);
        }
    }
}
