using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace Scan.App
{
    public class ClockAttachedProperty : BaseAttachedProperty<ClockAttachedProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var button = (sender as Button);
            button.Content = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");

            RoutedEventHandler onLoaded = null;
            onLoaded = (ss, ee) =>
            {
                button.Loaded -= onLoaded;
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += (s, evt) => {
                    button.Content = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
                };
                timer.Start();
            };
            button.Loaded += onLoaded;

        }
    }
}
