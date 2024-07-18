# Stock-Calculator
This is an extension on a project currently in development. It's a calculator used to measure important metrics when trading stocks. Includes proceeds, costs, net profit, return on investment, break even point, and desired profit.

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
            
  ----  Code-Behind:  ----

      private void ProfitCalculator_Click(object sender, RoutedEventArgs e)
        {
            CalculatorView calculatorView = new CalculatorView();
            calculatorView.ShowDialog();
        }
        
<p align="center">
      <img src="https://raw.github.com/jetompki/Stock-Calculator/master/StockCalculator-rev1.1.PNG"></img>
</p>


Note: 

If you'd like to test this project without going through the above steps, I've included a viewer in the solution. Ensure that the CalculatorViewer is marked as your StartUp Project and run the program from Visual Studio. Alternatively, build the solution and navigate to the bin/Debug directory of the CalculatorViewer and run the *CalculatorViewer.exe* file.
