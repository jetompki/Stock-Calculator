using System;
using System.Windows.Data;
using System.Windows.Media;

namespace Argin.Extensions.Converters
{
    public class DoubleToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);

            if (value == null) return brush;

            if ((double)value < 0.0)
            {
                brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#990000"));
            }
            else if ((double)value > 0.0)
            {
                brush = new SolidColorBrush(Colors.Green);
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
