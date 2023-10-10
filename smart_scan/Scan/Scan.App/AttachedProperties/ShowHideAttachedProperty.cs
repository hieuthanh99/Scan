using Scan.App.Animation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scan.App
{
    public class MonitorShowHideAttachedProperty<Parent> : BaseAttachedProperty<Parent, bool>
         where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        public bool FirstLoad { get; set; } = true;
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                return;

            if (FirstLoad)
            {
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    element.Loaded -= onLoaded;

                    DoAnimation(element, (bool)value);

                    FirstLoad = false;
                };

                element.Loaded += onLoaded;
            }
            else
                DoAnimation(element, (bool)value);
        }

        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }

    public class ShowHideAttachedProperty : MonitorShowHideAttachedProperty<ShowHideAttachedProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
                await element.SlideAndFadeInFromRightAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            else
                await element.SlideAndFadeOutToRightAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class HideToLeftAttachedProperty : MonitorShowHideAttachedProperty<HideToLeftAttachedProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            }
            else
            {
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            }
        }
    }

    public class HideToRightAttachedProperty : MonitorShowHideAttachedProperty<HideToRightAttachedProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeOutToRightAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            }
            else
            {
                await element.SlideAndFadeInFromRightAsync(FirstLoad ? 0 : 0.3f, keepMargin: false);
            }
        }
    }
}
