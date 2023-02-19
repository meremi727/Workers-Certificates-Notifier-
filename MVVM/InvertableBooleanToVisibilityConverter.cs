using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WpfApp1.MVVM
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class InvertableBooleanToVisibilityConverter : IValueConverter
    {
        enum Direction
        {
            Normal,
            Inverted
        }

        public object Convert(object value,
                              Type targetType,
                              object parameter,
                              CultureInfo culture)
        {
            var boolValue = (bool)value;
            var direction = GetDirectionOrDefault(parameter);
            if (direction is Direction.Inverted)
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value,
                                  Type targetType,
                                  object parameter,
                                  CultureInfo culture)
        {
            var visibility = (Visibility)value;
            var direction = GetDirectionOrDefault(parameter);
            if(direction is Direction.Inverted)
            {
                return visibility is not Visibility.Visible;
            }
            return visibility is Visibility.Visible;
        }

        private static Direction GetDirectionOrDefault(object? parameter) 
            => Enum.Parse<Direction>(parameter as string ?? Direction.Normal.ToString());
    }
}
