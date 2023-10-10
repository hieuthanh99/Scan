using Scan.Core;
using Scan.Core.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scan.App;

public class BaseWindow<VM> : Window where VM : BaseViewModel, new()
{
    private VM mViewModel;
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
    public BaseWindow()
    {

        this.Loaded += BasePage_Loaded;
        ////ConstructorInfo ctor = typeof(VM).GetConstructor(new[] { typeof(Window) });
        this.ViewModel = (VM)Activator.CreateInstance(typeof(VM), this);
    }

    private void BasePage_Loaded(object sender, RoutedEventArgs e)
    {
        //Do nothing
    }
}
