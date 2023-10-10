using Scan.Core;
using Scan.Core.Settings;
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
    /// Interaction logic for MainSidebar.xaml
    /// </summary>
    public partial class MainSidebar : UserControl
    {
        public MainSidebar()
        {
            InitializeComponent();

            // check show/hide menu
            UserInfo user = IoC.Get<ApplicationViewModel>().CurrentUser;
            if(user == null)
            {
                IoC.Get<ApplicationViewModel>().GotoPage(ApplicationPage.Login);
            }
        }
    }
}
