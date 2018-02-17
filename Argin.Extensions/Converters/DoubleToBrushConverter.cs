using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Argin.Extensions.Converters
{
    public class DoubleToBrushConverter : IValueConverter
    {
        private ResourceDictionary _resourceDictionary;
        public ResourceDictionary ResourceDictionary
        {
            get { return _resourceDictionary; }
            set
            {
                _resourceDictionary = value;
            }
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush brush = new SolidColorBrush(Colors.Black);

            if (value == null) return brush;

            if ((double)value < 0.0)
            {
                brush = ResourceDictionary["NegativeLossBrush"] as SolidColorBrush;
            }
            else if ((double)value > 0.0)
            {
                brush = ResourceDictionary["PositiveGainBrush"] as SolidColorBrush;
            }

            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
