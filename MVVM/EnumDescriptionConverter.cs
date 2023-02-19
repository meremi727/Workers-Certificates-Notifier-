using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.MVVM
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public static string GetEnumDescription(Enum enumObj)
        {
            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            object[]? attribArray = fieldInfo?.GetCustomAttributes(false);

            if (attribArray is null || attribArray.Length == 0)
            {
                return enumObj.ToString();
            }
            DescriptionAttribute? attrib = attribArray[0] as DescriptionAttribute;
            return attrib?.Description ?? string.Empty;
        }

        object IValueConverter.Convert(object value,
                                       Type targetType,
                                       object parameter,
                                       CultureInfo culture)
        {
            string description = GetEnumDescription((Enum)value);
            return description;
        }

        object IValueConverter.ConvertBack(object value,
                                           Type targetType,
                                           object parameter,
                                           CultureInfo culture)
            => string.Empty;
    }
}
