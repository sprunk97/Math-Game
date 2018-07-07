using System;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Media;

namespace MathGame
{
    public class CellBackGroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string input = value as string;
            if (Regex.IsMatch(input, ".*\\(.*\\).*"))
                return Brushes.LightPink;
            else return Brushes.LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
} 