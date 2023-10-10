using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scan.App
{
    public class BooleanToVisiblityConverterVer2 : BaseValueConverter<BooleanToVisiblityConverterVer2>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (bool)value ? Visibility.Collapsed : Visibility.Visible;
            else
                return (bool)value ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class StringToVisiblityConverterVer2 : BaseValueConverter<StringToVisiblityConverterVer2>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return !string.IsNullOrEmpty((string)value) ? Visibility.Collapsed : Visibility.Visible;
            else
                return !string.IsNullOrEmpty((string)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
    public class StringToVisiblityConverterVer3 : BaseValueConverter<StringToVisiblityConverterVer3>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return !string.IsNullOrEmpty((string)value) ? Visibility.Visible : Visibility.Hidden;
            else
                return !string.IsNullOrEmpty((string)value) ? Visibility.Hidden : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }

    public class IntToVisiblityConverterVer : BaseValueConverter<IntToVisiblityConverterVer>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (int)value > 1 ? Visibility.Collapsed : Visibility.Visible;
            else
                return (int)value <= 1 ? Visibility.Visible : Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }

    public class IntToVisiblityConverterVer1 : BaseValueConverter<IntToVisiblityConverterVer1>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var listObject = value as System.Collections.ObjectModel.ObservableCollection<Object>;
            int count = listObject.Count;
            if (parameter == null)
                if (count == 0) return Visibility.Collapsed;
                else if (count > 1) return Visibility.Collapsed;
                else
                {
                    var contractFile = listObject.ElementAt(0) as Scan.Core.DTO.ImageDTO;
                    if (string.IsNullOrEmpty(contractFile.ServerId)) return Visibility.Visible;
                    return Visibility.Collapsed;
                }

            else
                if (count == 0) return Visibility.Collapsed;
            else if (count > 1) return Visibility.Collapsed;
            else
            {
                var contractFile = listObject.ElementAt(0) as Scan.Core.DTO.ImageDTO;
                if (string.IsNullOrEmpty(contractFile.ServerId)) return Visibility.Visible;
                return Visibility.Collapsed;
            }

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
