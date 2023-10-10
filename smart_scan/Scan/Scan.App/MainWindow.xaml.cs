using log4net;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BaseWindow<WindowViewModel>
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MainWindow));

        public MainWindow() : base()
        {
            try
            {
                InitializeComponent();
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            int index = ((Menu)menuItem.Parent).Items.IndexOf(menuItem);

            switch (index)
            {
                case 0:
                    RedirectHelper.RedirectLoading(ApplicationPage.Home);
                    break;
                case 1:
                    RedirectHelper.RedirectLoading(ApplicationPage.ListCapture);
                    break;
                case 2:
                    RedirectHelper.RedirectLoading(ApplicationPage.ScanNoLogin);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

    }
}
