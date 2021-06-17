﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DspHelper.Views.Converters
{
    public class ItemEditingToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is not null && values[1] is not null)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
