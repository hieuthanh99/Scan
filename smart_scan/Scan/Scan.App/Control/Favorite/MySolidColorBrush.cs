using Scan.Core;
using System.Windows.Media;

namespace Scan.App
{
    internal class MySolidColorBrush : MySolidBrush
    {
        private Color randomColor;

        public MySolidColorBrush(Color randomColor)
        {
            this.randomColor = randomColor;
        }
    }
}