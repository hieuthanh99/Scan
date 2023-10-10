using Scan.App.Animation;
using Scan.Core;
using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Scan.App
{
    public class BasePage<VM> : Page where VM : BaseViewModel, new()
    {
        private VM mViewModel;
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;
        public PageAnimation PageUnLoadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;
        public float SlideSeconds { get; set; } = 0.8f;

        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                if (mViewModel == value) return;
                mViewModel = value;
                this.DataContext = mViewModel;
            }
        }

        public BasePage()
        {
            this.ViewModel = new VM();
        }

        public async Task AnimateIn()
        {
            if (this.PageLoadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (this.PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    {
                        await this.SlideAndFadeInFromRight(this.SlideSeconds);
                        break;
                    }
            }
        }

        public async Task AnimateOut()
        {
            if (this.PageUnLoadAnimation == PageAnimation.None)
            {
                return;
            }

            switch (this.PageUnLoadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:
                    {
                        await this.SlideAndFadeOutToLeft(this.SlideSeconds);
                        break;
                    }
            }
        }
    }
}
