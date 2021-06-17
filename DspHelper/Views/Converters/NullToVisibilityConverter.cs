using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DspHelper.Views.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? Visibility.Collapsed : Visibility.Visible;
            //Visibility visibility = value is null ? Visibility.Collapsed : Visibility.Visible;
            //return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
