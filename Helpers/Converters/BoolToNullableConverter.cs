using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Northboundei.Mobile.Helpers.Converters
{

    public class BoolToNullableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? currentValue = value as bool?;

            string targetState = parameter as string;

            return targetState switch
            {
                "True" => currentValue == true,
                "False" => currentValue == false,
                "null" => currentValue == null,
                _ => false
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Only proceed if the RadioButton is checked (value is true)
            if (value is bool isChecked && isChecked && parameter is string targetState)
            {
                return targetState switch
                {
                    "True" => true,
                    "False" => false,
                    "null" => (bool?)null,
                    _ => (bool?)null
                };
            }
            return Binding.DoNothing;
        }
    }

}
