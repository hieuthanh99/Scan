using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Scan.App
{
    public class ColorConverter : IValueConverter
    {
        private static readonly Random _random = new Random();
        private static readonly List<Color> _colors = new List<Color>
          {
          Color.FromRgb(236, 170, 170),  // "#ECAAAA"
          Color.FromRgb(136, 180, 221),  // "#88B4DD"
          Color.FromRgb(160, 222, 111),  // "#AAE8E8"
          Color.FromRgb(191, 243, 190)   // "#BFF3BE"
          };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush(_colors[_random.Next(_colors.Count)]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
