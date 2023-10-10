using log4net;
using Scan.Core.Services;
using Scan.Core.Settings;
using Scan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scan.App
{
    /// <summary>
    /// Interaction logic for BottomSideBarControl.xaml
    /// </summary>
    public partial class BottomSideBarControl : UserControl
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BottomSideBarControl));

        public BottomSideBarControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
         
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IoC.Get<ApplicationViewModel>().IsShowNotifyUpdate = false;
        }
    }
}
