using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace GestioneCopie.Common.Resources.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = false;

            if (value == null)
            {
                result = false;
            }
            else if (value is int)
            {
                result = int.Parse(value.ToString()) > 0 ? true : false;
            }
            else if (value is string)
            {
                result = !string.IsNullOrEmpty(value.ToString()) && !value.ToString().Equals("0");
            }
            else
            {
                result = true;
            }

            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibile = (Visibility)value == Visibility.Visible;

            if (targetType == typeof(string))
            {
                return visibile.ToString();
            }

            else if (targetType == typeof(int))
            {
                return visibile ? 1 : 0;
            }
            else
                return visibile;
        }
    }
}
