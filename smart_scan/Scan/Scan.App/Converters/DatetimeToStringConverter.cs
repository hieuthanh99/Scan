using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public class DatetimeToStringConverter : BaseValueConverter<DatetimeToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"Truy cập lúc: {value.ToString()}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateToStringConverter : BaseValueConverter<DateToStringConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((DateTime)value).ToString((string)parameter, culture);

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueStr = (string)value;
            string paramStr = (string)parameter;

            DateTime? time = null;
            try
            {
                if (!string.IsNullOrEmpty(valueStr))
                {
                    time = DateTime.ParseExact(valueStr.Trim(), paramStr, culture);
                }
            }
            catch (Exception) { }

            return time;
        }
    }
}
