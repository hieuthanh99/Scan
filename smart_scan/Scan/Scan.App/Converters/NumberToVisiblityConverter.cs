using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scan.App
{
    public class NumberToVisiblityConverter : BaseValueConverter<NumberToVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == 0 ? Visibility.Hidden : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class NumberLevelToVisiblityConverter : BaseValueConverter<NumberLevelToVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (int)value <= 0 ? Visibility.Hidden : Visibility.Visible;
            else
            {
                int number;
                Int32.TryParse((string)parameter, out number);
                return (int)value >= number ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class NumberToStringConverter : BaseValueConverter<NumberToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return ((int)value).ToString();
            }

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueStr = (string)value;
            int number = 0;

            Int32.TryParse(valueStr, out number);

            return number;
        }
    }
}
