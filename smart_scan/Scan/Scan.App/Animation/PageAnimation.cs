using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows;

namespace Scan.App.Animation
{
    public enum PageAnimation
    {
        None = 0,
        SlideAndFadeInFromRight = 1,
        SlideAndFadeOutToLeft = 2
    }

    public static class PageAnimations
    {
        public static async Task SlideAndFadeInFromRight(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideFromRight(seconds, page.WindowWidth);
            sb.AddFadeIn(seconds);
            sb.Begin(page);
            page.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }

        public static async Task SlideAndFadeOutToLeft(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideToLeft(seconds, page.WindowWidth);
            sb.AddFadeOut(seconds);
            sb.Begin(page);
            page.Visibility = Visibility.Visible;

            await Task.Delay((int)seconds * 1000);
        }
    }
}
