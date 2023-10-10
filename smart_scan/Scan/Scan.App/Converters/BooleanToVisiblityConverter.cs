using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scan.App
{
    public class BooleanToVisiblityConverter : BaseValueConverter<BooleanToVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? Visibility.Hidden : Visibility.Visible;
            else
                return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class BooleanAndOperatorConverter : BaseMultiValueConverter<BooleanAndOperatorConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value == false)
                {
                    return false;
                }
            }
            return true;
        }

        public override object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanAndOperatorConverter is a OneWay converter.");
        }
    }

    public class BooleanOrOperatorConverter : BaseMultiValueConverter<BooleanOrOperatorConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value)
                {
                    return true;
                }
            }
            return false;
        }

        public override object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("BooleanOrOperatorConverter is a OneWay converter.");
        }
    }

    public class NegationBooleanOrOperatorConverter : BaseMultiValueConverter<NegationBooleanOrOperatorConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (object value in values)
            {
                if ((value is bool) && (bool)value)
                {
                    return false;
                }
            }
            return true;
        }

        public override object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("NegationBooleanOrOperatorConverter is a OneWay converter.");
        }
    }
}
