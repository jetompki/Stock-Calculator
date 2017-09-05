# Stock-Calculator
This is an extension on a project currently in development. It is a calculator used to measure important metrics when trading stocks. Includes proceeds, costs, net profit, return on investment, break even point, and desired profit.

Use:

1. Add Argin.Extensions to your solution.
2. Provide reference to Argin.Extensions from a project of your choice.
3. Include the following using:

       using Argin.Extensions.ProfitCalculator.Views;
    
4. Call the following to open the window:

       CalculatorView calculatorView = new CalculatorView();
       calculatorView.ShowDialog();

Example:
      
  ----  WPF: ----

           <Menu Background="LightGray">
                <MenuItem Header="Tools" Height="25">
                    <MenuItem Header="Profit Calculator" Click="ProfitCalculator_Click"/>
                </MenuItem>
            </Menu>
            
  ----  Code-behind:  ----

      private void ProfitCalculator_Click(object sender, RoutedEventArgs e)
        {
            CalculatorView calculatorView = new CalculatorView();
            calculatorView.ShowDialog();
        }

