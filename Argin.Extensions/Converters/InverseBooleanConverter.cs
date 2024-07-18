using System;
using System.Globalization;
using System.Windows.Data;

namespace Argin.Extensions.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool testValue = (bool)value;
                return !testValue;
            }
            catch { return true; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
}
