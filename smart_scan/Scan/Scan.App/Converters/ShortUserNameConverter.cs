using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scan.App
{
    public class ShortUserNameConverter : BaseValueConverter<ShortUserNameConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string userName = value.ToString();
            var arrUserName = Regex.Split(userName, @"[._\-]");
            string shortName = "";
            if (arrUserName.Length > 0)
            {
                var name = arrUserName[0];
                if (name.Length <= 3)
                {
                    shortName = name;
                }
                else
                {
                    shortName = name.Substring(0, 3);
                }
            }

            return shortName.ToUpper();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
