using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SUAIdashboard.Converter
{
    public class VisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)values[0];
            Visibility visibility2 = (Visibility)values[1];

            if (visibility == Visibility.Visible)
            {
                return Visibility.Visible;
            }
            else
            {
                if (visibility2 == Visibility.Collapsed)
                {
                    return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
