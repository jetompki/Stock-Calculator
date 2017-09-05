using System;
using System.Windows;
using System.Windows.Data;

namespace Argin.Extensions.Resources.ConvertersForResources
{
    public class PointRelativeWidthHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || parameter == null) return new Point(0, 0);

            string[] param = (parameter as string).Split('|');
            double xValue = (double)values[0];
            double yValue = (double)values[1];

            double paramX = 0.0;
            double paramY = 0.0;

            switch (param[0])
            {
                case "Quadrant1":
                    xValue = xValue * 0.75;
                    yValue = yValue * 0.25;
                    break;
                case "Quadrant2":
                    xValue = xValue * 0.25;
                    yValue = yValue * 0.25;
                    break;
                case "Quadrant3":
                    xValue = xValue * 0.25;
                    yValue = yValue * 0.75;
                    break;
                case "Quadrant4":
                    xValue = xValue * 0.75;
                    yValue = yValue * 0.75;
                    break;
                default:
                    bool success = Double.TryParse(param[0], out paramX);

                    if (!success) throw new Exception("[PointRelativeWidthHeightConverter] -> Invalid parameter. Valid parameters are: Quadrant1, Quadrant2, Quadrant3, Quadrant4 or double|double. Example - 0.75|0.25");

                    success = Double.TryParse(param[1], out paramY);

                    if (!success) throw new Exception("[PointRelativeWidthHeightConverter] -> Invalid parameter. Valid parameters are: Quadrant1, Quadrant2, Quadrant3, Quadrant4 or double|double. Example - 0.75|0.25");

                    xValue = xValue * paramX;
                    yValue = yValue * paramY;
                    break;
            }

            return new Point(xValue, yValue);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
