using DspHelper.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace DspHelper.Views.Converters
{
    public class DspRecipeValueToInputFieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == int.MinValue)
                return "?";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "?")
                return int.MinValue;

            return int.Parse((string)value);
        }
    }
}
