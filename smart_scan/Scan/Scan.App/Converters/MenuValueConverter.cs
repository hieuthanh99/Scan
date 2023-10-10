using Scan.Core.Settings;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan.App
{
    public class MenuValueConverter : BaseValueConverter<MenuValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.Login:
                case ApplicationPage.ScanNoLogin:
                    return new ListUserHistoryLogin();
                case ApplicationPage.Home:
                case ApplicationPage.ListCapture:
                    return new MainSidebar();
                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
