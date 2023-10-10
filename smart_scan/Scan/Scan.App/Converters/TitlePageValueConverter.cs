using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public class TitlePageValueConverter : BaseValueConverter<TitlePageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sReturn = "";
            AppConfig.TitileApp.TryGetValue((ApplicationPage)value, out sReturn);
            return sReturn;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
